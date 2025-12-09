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


# ----------------------------
# Boucle de lecture
# ----------------------------
try:
    while True:
        son = lire_mcp3008(0)        # Capteur sonore sur CH0
        lumiere = lire_mcp3008(1)    # Capteur lumière sur CH1

        print(f"Son (CH0) : {son:4d}   |   Lumière (CH1) : {lumiere:4d}")
        # Date et heure actuelles
        now = datetime.now()
        cursor = conn.cursor()
        # Insertion avec datetime
        # cursor.execute(
        cursor.execute(
        "INSERT INTO Donnees (valLumiere, valSon, dateHeure, noUtilisateur) "
        "VALUES ( %d, %d, %s, %d)",
        (lumiere, son, now, 46)
    )

        conn.commit()  
        time.sleep(2)

except KeyboardInterrupt:
    print("\nArrêt du programme.")