# 📘 ReadMe – Doom vs Fortnite

## 🎮 Spielübersicht
Dieses Projekt ist ein First-Person-Shooter in Unity mit **schnellem Movement**, **Kampf gegen KI-Gegner (Bots)** und **interaktiver Umgebung**.  
Es kombiniert klassische Shooter-Elemente mit modernen Movement-Mechaniken im Stil von **Doom Eternal**

Link: https://youtu.be/da0aGUgbBC4



---

## 🚀 Funktionen

### 👤 Spieler (Advanced First Person Controller)

#### 🧍 Standardsteuerung
- First Person Kamera + Bewegung
- Fadenkreuz basiert auf Kameramitte
- Mausgesteuertes Zielen
- Collider + Rigidbody basierte Physik

### 💥 Kampf
- **Schießen mit Projektilwaffe**
  - Bullet fliegt dahin, wo die Kamera hinzielt (Fadenkreuz)
  - Bullet kann Schaden an Gegner und Spieler verursachen
- **Bomben werfen**
  - Physikbasierter Wurf
  - Explosion nach Delay
  - Schadensradius
  - Explosionseffekt + optional Sound

### 🧠 Treffer-Feedback

- **Roter Bildschirm-Flash** (DamageFlash)


## 🤖 Gegner (Bots)
- Navigieren mit NavMeshAgent zum Spieler
- Zielverfolgung mit Drehung zur Kamera
- Stoppen in Reichweite und gezieltes Schießen
- Animationen (Laufen, Schießen, Getroffen, Sterben)
- Zerstörung bei 0 HP

## 🧗 Erweiterte Bewegung (DOOM-like)

### 🔹 Double Jump
- Spieler kann in der Luft bis zu drei Mal springen
- Reset beim Bodenkontakt

### 🔹 Dash
- Dash in Blickrichtung (nach vorne)
- Cooldown-Steuerung für fairen Einsatz
- Nutzt Rigidbody-Impuls oder CharacterController-Movement

### 🔹 Grappling Hook / Spiderman-Seil
- Spieler zieht sich per Physik dorthin
- Raycast-basierte Zielerkennung

### 🔹 Trampoline
- Trigger-Zone
- Katapultiert Spieler vertikal in die Luft
- Kann kombiniert werden mit Double Jump für mehr Höhe

### 🔹 Wall Climb & Wall Jump
- Spieler erkennt „kletterbare“ Wände per Tag oder Layer
- Festhalten an Wand mit Timer
- Absprung von Wand in Blickrichtung
- Funktioniert auch in der Luft

## 🎆 VFX & Materialien
- **Emission-Materialien** für Bomben oder gefährliche Objekte
- **Pulse-Effekt** für tickende Bomben
