import customtkinter as ctk
import InterfacePrincipalIot 
from tkinter import messagebox
from PIL import Image
import spidev
import time

import pymssql
from datetime import datetime

ctk.set_appearance_mode("dark")
ctk.set_default_color_theme("blue")


try:
    conn = pymssql.connect(
        server="dicjwin01.cegepjonquiere.ca",  # IP du SQL Server
        user="prog3e05",
        password="visage30",
        database="Prog3A25_AllysonJad"
    )


except Exception as e:
    print("Erreur connexion :", e)

class App(ctk.CTk):
    def __init__(self):
        super().__init__()
        self.title("Mini-Onyra")
        self.geometry("700x450")  # plus grand pour l'image
        self.page_accueil = Accueil(self)
        self.page_login = LoginPage(self)
        self.page_data = None

        self.show_page(self.page_accueil)

    def show_page(self, page):
        for widget in self.winfo_children():
            widget.pack_forget()
        page.pack(fill="both", expand=True)


class BackgroundPage(ctk.CTkFrame):
    def __init__(self, master, image_path):
        super().__init__(master)

        # Charger l'image
        bg_img = ctk.CTkImage(
            light_image=Image.open(image_path),
            dark_image=Image.open(image_path),
            size=(700, 450)  # dimension de la fenêtre
        )

        # Image affichée en plein écran
        self.bg_label = ctk.CTkLabel(self, image=bg_img, text="")
        self.bg_label.place(x=0, y=0, relwidth=1, relheight=1)


class Accueil(BackgroundPage):
    def __init__(self, master):
        super().__init__(master, "design.png")  # <-- image sommeil

        ctk.CTkLabel(self, text="Bienvenue à Onyra",
                     font=("Calibri", 32), bg_color="transparent").pack(pady=40)

        ctk.CTkButton(
            self,
            text="Découvrir",
            font=("Calibri", 20),
            width=220,
            height=50,
            command=lambda: master.show_page(master.page_login)
        ).pack(pady=20)


class LoginPage(BackgroundPage):
    def __init__(self, master):

        super().__init__(master, "design.png")  # <-- même image
        25a4c3bfe110c5177b06f01e392b0392e8ce884b

        ctk.CTkLabel(self, text="Connexion",
                     font=("Arial", 30), bg_color="transparent").pack(pady=30)

        self.username = ctk.CTkEntry(self, placeholder_text="Nom d'utilisateur",
                                     font=("Arial", 18), width=260, height=40)
        self.username.pack(pady=10)

        self.password = ctk.CTkEntry(self, placeholder_text="Mot de passe",
                                     font=("Arial", 18), width=260, height=40, show="•")
        self.password.pack(pady=10)

        ctk.CTkButton(self, text="Se connecter", font=("Arial", 20),
                      width=200, height=45,
                      command=lambda: self.VerifyLogin(master)).pack(pady=20)

    def VerifyLogin(self, master):
        email = self.username.get()
        password = self.password.get()

        conn = pymssql.connect(
            server="dicjwin01.cegepjonquiere.ca",
            user="prog3e05",
            password="visage30",
            database="Prog3A25_AllysonJad"
        )
        cursor = conn.cursor()
        
        cursor.execute("""
            DECLARE @noUtiliateur INT;
            EXEC UP_ConnexionUtilisateur @courriel=%s, @motDePasse=%s, @noUtiliateur=@noUtiliateur OUTPUT;
            SELECT @noUtiliateur;
        """, (email, password))

        row = cursor.fetchone()
        cursor.close()
        conn.close()

        if row and row[0] is not None and row[0] > 0:
            user_id = row[0]
            messagebox.showinfo("Succès", "Connexion réussie !")

            # ⚡ CRÉER LA PAGE avec le user_id
            master.page_data = InterfacePrincipalIot.InterfaceCapteurs(master, user_id)

            master.show_page(master.page_data)

        else:
            messagebox.showerror("Erreur", "Identifiants incorrects.")
            self.username.delete(0, "end")
            self.password.delete(0, "end")



class HomePage(BackgroundPage):
    def __init__(self, master):
        super().__init__(master, "design.png")

        ctk.CTkLabel(self, text="Bienvenue !",
                     font=("Arial", 30), bg_color="transparent").pack(pady=40)

        ctk.CTkButton(
            self,
            text="Déconnexion",
            font=("Arial", 20),
            width=200,
            height=50,
            command=lambda: master.show_page(master.page_login)
        ).pack(pady=20)


app = App()
app.mainloop()
