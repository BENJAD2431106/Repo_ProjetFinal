import customtkinter as ctk
import random
from datetime import datetime
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
import matplotlib.pyplot as plt

# ----------------------------
#   DONNÉES BIDON
# ----------------------------

import spidev
import time

import pymssql
from datetime import datetime

try:
    conn = pymssql.connect(
        server="dicjwin01.cegepjonquiere.ca",  # IP du SQL Server
        user="prog3e05",
        password="visage30",
        database="Prog3A25_AllysonJad"
    )


except Exception as e:
    print("Erreur connexion :", e)

# ----------------------------
# Initialisation SPI
# ----------------------------
spi = spidev.SpiDev()
spi.open(0, 0)           # Bus 0, Device 0 (CE0)
spi.max_speed_hz = 1350000
def lire_mcp3008(channel):
    """Lecture d'un canal (0-7) du MCP3008."""
    adc = spi.xfer2([1, (8 + channel) << 4, 0])
    return ((adc[1] & 3) << 8) + adc[2]


def lire_capteur_lumiere():
    lumiere = lire_mcp3008(1) 
    return lumiere

def lire_capteur_son():
    son = lire_mcp3008(0) 
    return son

# ----------------------------
#       INTERFACE
# ----------------------------
class InterfaceCapteurs(ctk.CTkFrame):
    def __init__(self, master, user_id):
        super().__init__(master)
        self.user_id = user_id  # <-- ON LE GARDE ICI
        self.running = False
        self.refresh_rate = 2000  # ms
        self.smooth_steps = 20
        self.data = []
        self.initialTime = datetime.now()


        # -------------------------
        # TIMER SECONDES
        # -------------------------
        self.elapsed_seconds = 0
        self.compteur_running = False
        # -------------------------
        # TITRE
        # -------------------------
        self.label_titre = ctk.CTkLabel(self, text="Monitoring en Temps Réel",
                                        font=("Arial", 32, "bold"))
        self.label_titre.pack(pady=20)

        # -------------------------
        # FRAME GLOBAL
        # -------------------------
        self.frame_global = ctk.CTkFrame(self, corner_radius=20)
        self.frame_global.pack(padx=20, pady=10, fill="both", expand=True)

        # ENCADRÉ LUMIÈRE
        self.frame_lumiere = ctk.CTkFrame(self.frame_global, corner_radius=15)
        self.frame_lumiere.pack(pady=10, padx=20, fill="x")

        self.label_lumiere_title = ctk.CTkLabel(self.frame_lumiere, text="Capteur Lumière (lux)",
                                                font=("Arial", 26, "bold"))
        self.label_lumiere_title.pack(pady=8)

        self.label_lumiere_value = ctk.CTkLabel(self.frame_lumiere, text="-- lux", font=("Arial", 32))
        self.label_lumiere_value.pack(pady=5)

        self.jauge_lumiere = ctk.CTkProgressBar(self.frame_lumiere, height=25)
        self.jauge_lumiere.pack(pady=10, padx=30, fill="x")
        self.jauge_lumiere.set(0)

        # ENCADRÉ SON
        self.frame_son = ctk.CTkFrame(self.frame_global, corner_radius=15)
        self.frame_son.pack(pady=10, padx=20, fill="x")

        self.label_son_title = ctk.CTkLabel(self.frame_son, text="Capteur Son (Hz)",
                                            font=("Arial", 26, "bold"))
        self.label_son_title.pack(pady=8)

        self.label_son_value = ctk.CTkLabel(self.frame_son, text="-- Hz", font=("Arial", 32))
        self.label_son_value.pack(pady=5)

        self.jauge_son = ctk.CTkProgressBar(self.frame_son, height=25)
        self.jauge_son.pack(pady=10, padx=30, fill="x")
        self.jauge_son.set(0)

        # -------------------------
        # BOUTONS
        # -------------------------
        self.frame_btns = ctk.CTkFrame(self, corner_radius=10)
        self.frame_btns.pack(pady=10)

        self.bouton_start = ctk.CTkButton(self.frame_btns, text="Démarrer", width=160,
                                          command=self.demarrer)
        self.bouton_start.grid(row=0, column=0, padx=10)

        self.bouton_pause = ctk.CTkButton(self.frame_btns, text="Pause", width=160,
                                          command=self.pause)
        self.bouton_pause.grid(row=0, column=1, padx=10)

        self.bouton_stop = ctk.CTkButton(self.frame_btns, text="Terminer capture", width=160,
                                         fg_color="orange", hover_color="#ffaa00",
                                         command=self.stop_capture)
        self.bouton_stop.grid(row=0, column=2, padx=10)

        # -------------------------
        # STATUS ET TIMER
        # -------------------------
        self.status = ctk.CTkLabel(self, text="En attente...", font=("Arial", 18))
        self.status.pack(pady=5)

        self.label_timer = ctk.CTkLabel(self, text="Temps écoulé : 0 s", font=("Arial", 18))
        self.label_timer.pack(pady=5)

        # -------------------------
        # NOUVEAU LABEL INFO (FIXE)
        # -------------------------
        self.label_info = ctk.CTkLabel(self,
                                       text="ℹ Attention : Lumière > 200 lux ou Son > 300 Hz",
                                       font=("Arial", 16),
                                       text_color="yellow")
        self.label_info.pack(pady=5)

        # -------------------------
        # LABEL WARNING
        # -------------------------
        self.label_warning = ctk.CTkLabel(self, text="", font=("Arial", 18))
        self.label_warning.pack(pady=5)

        # -------------------------
        # FRAME TABLEAU + GRAPHIQUE
        # -------------------------
        self.frame_data_graph = ctk.CTkFrame(self, corner_radius=15)
        self.frame_data_graph.pack(padx=20, pady=10, fill="both", expand=True)

        # Tableau à gauche
        self.frame_table = ctk.CTkScrollableFrame(self.frame_data_graph, corner_radius=15)
        self.frame_table.pack(side="left", padx=10, pady=10, fill="both", expand=True)
        self.labels_table = []

        # Graphique à droite
        self.fig, self.ax = plt.subplots(figsize=(6,4), facecolor="#2b2b2b")
        self.ax.set_facecolor("#2b2b2b")
        self.ax.tick_params(colors='white', labelcolor='white')
        self.ax.spines['bottom'].set_color('white')
        self.ax.spines['top'].set_color('white')
        self.ax.spines['left'].set_color('white')
        self.ax.spines['right'].set_color('white')
        self.ax.title.set_color('white')
        self.ax.xaxis.label.set_color('white')
        self.ax.yaxis.label.set_color('white')
        self.canvas = FigureCanvasTkAgg(self.fig, master=self.frame_data_graph)
        self.canvas.get_tk_widget().pack(side="right", fill="both", expand=True)

        # -------------------------
        # ENCADRÉ MOYENNE
        # -------------------------
        self.frame_moyenne = ctk.CTkFrame(self, corner_radius=15)
        self.frame_moyenne.pack(pady=10, padx=20, fill="x")

        self.label_moyenne = ctk.CTkLabel(self.frame_moyenne,
                                          text="Moyennes : Lumière = -- lux, Son = -- Hz",
                                          font=("Arial", 20, "bold"))
        self.label_moyenne.pack(pady=10)

        # valeurs actuelles
        self.current_lumiere = 0
        self.current_son = 0

    # -------------------------
    # Smooth update
    # -------------------------
    def smooth_update(self, current, target):
        delta = (target - current) / self.smooth_steps
        return current + delta

    # -------------------------
    # COMPTEUR SECONDES INDÉPENDANT
    # -------------------------
    def update_compteur(self):
        if self.compteur_running:
            self.elapsed_seconds += 1
            self.label_timer.configure(text=f"Temps écoulé : {self.elapsed_seconds} s")
        else:
            self.after(1000, self.update_compteur)

    # -------------------------
    # UPDATE DONNÉES
    # -------------------------
    def update_donnees(self):
        if not self.running:
            return

        lumiere = lire_capteur_lumiere()
        son = lire_capteur_son()

        self.current_lumiere = lumiere
        self.label_lumiere_value.configure(text=f"{int(self.current_lumiere)} lux")
        self.jauge_lumiere.set(min(self.current_lumiere / 500, 1))

        self.current_son = son
        self.label_son_value.configure(text=f"{int(self.current_son)} Hz")
        self.jauge_son.set(min((self.current_son - 20) / 1000, 1))

        # -------------------------------
        # WARNING (ce message continue de changer)
        # -------------------------------
        if lumiere > 200 or son > 300:
            self.label_warning.configure(text="⚠ Conditions non idéales", text_color="red")
        else:
            self.label_warning.configure(text="✓ Vous dormez dans de bonnes conditions", text_color="green")

        # Ajouter la donnée dans la table
        timestamp = datetime.now().strftime("%H:%M:%S")
        self.data.append((timestamp, int(self.current_lumiere), int(self.current_son)))
        self.rafraichir_table()
        self.update_graph()

        self.after(self.refresh_rate, self.update_donnees)
        sentTimetoDB = datetime.now()
        if (sentTimetoDB - self.initialTime).total_seconds() >= 60:
            self.initialTime=sentTimetoDB
            # Date et heure actuelles
            now = datetime.now()
            cursor = conn.cursor()
            # Insertion avec datetime
            # cursor.execute(
            cursor.execute(
            "INSERT INTO Donnees (valLumiere, valSon, dateHeure, noUtilisateur) "
            "VALUES ( %d, %d, %s, %d)",
            (lumiere, son, now, self.user_id))
            conn.commit()

    # -------------------------
    # Rafraîchir le tableau
    # -------------------------
    def rafraichir_table(self):
        for lbl in self.labels_table:
            lbl.destroy()
        self.labels_table = []

        header = ctk.CTkLabel(self.frame_table,
                               text=f"{'Heure':<10} | {'Lumière':<8} | {'Son':<5}",
                               font=("Arial", 16, "bold"))
        header.pack(anchor="w", padx=10)
        self.labels_table.append(header)

        for t, l, s in self.data[-50:]:
            lbl = ctk.CTkLabel(self.frame_table, text=f"{t:<10} | {l:<8} | {s:<5}",
                                font=("Arial", 14))
            lbl.pack(anchor="w", padx=10)
            self.labels_table.append(lbl)

    # -------------------------
    # UPDATE GRAPHIQUE GLISSANT
    # -------------------------
    def update_graph(self):
        if not self.data:
            return

        data_recent = self.data[-50:]
        times = [d[0] for d in data_recent]
        lumiere = [d[1] for d in data_recent]
        son = [d[2] for d in data_recent]

        self.ax.clear()
        self.ax.plot(times, lumiere, label="Lumière (lux)", color="orange", marker='o')
        self.ax.plot(times, son, label="Son (Hz)", color="skyblue", marker='o')
        self.ax.set_title("Lumière et Son", color='white')
        self.ax.set_xlabel("Temps", color='white')
        self.ax.set_ylabel("Valeurs", color='white')
        self.ax.legend()
        self.ax.tick_params(axis='x', rotation=0, colors='white')
        self.ax.set_facecolor("#2b2b2b")
        self.fig.tight_layout()
        self.canvas.draw()

    # -------------------------
    # COMMANDES
    # -------------------------
    def demarrer(self):
        if not self.running:
            self.running = True
            self.compteur_running = True
            self.elapsed_seconds = 0
            self.update_donnees()
            self.update_compteur()
            self.status.configure(text="Lecture en cours...")

    def pause(self):
        self.running = not self.running
        self.status.configure(text="Pause" if not self.running else "Lecture en cours")
        if self.running:
            self.update_donnees()

    def stop_capture(self):
        if self.running or self.compteur_running:
            self.running = False
            self.compteur_running = False
            self.status.configure(text="Capture terminée")
            if self.data:
                moyenne_lumiere = sum([d[1] for d in self.data]) / len(self.data)
                moyenne_son = sum([d[2] for d in self.data]) / len(self.data)
                self.label_moyenne.configure(
                    text=f"Moyennes : Lumière = {int(moyenne_lumiere)} lux, Son = {int(moyenne_son)} Hz"
                )

    def quitter(self):
        self.running = False
        self.compteur_running = False
        self.destroy()
