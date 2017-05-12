SpieleProgrammierungSoSe2017
--------------------------------------------------------------------------------
UPDATE 12.05.2017:
Anleitung zum todo
Alle Dateien, denen ihr die Dateiendung todo gebt, werden automatisch von dem
Atom-Plugin "tasks" in schöner Weise verarbeitet.
Eine Zeile, die von einem Doppelpunkt beendet wird, gilt als Überschrift.
Diese ist maßgeblich dafür, was beim Abhaken eines Punktes anotiert wird.
Daher stehen auch immer schön eure Namen hinter den Häkchen

- ein todo-Punkt wird durch Strg + Enter hinzugefügt
- ein todo-Punkt wird durch Strg + D abgehakt

Updates des Readmes am besten mit Datum einfach oben einfügen

Ich habe weiterhin den dev-vehicles gelöscht, da der im Moment nicht mehr aktiv
benötigt wird als Branch. Der ist bereits vollständig in Test_Game enthalten
und die Commits sind natürlich noch da.

Schaut euch vielleicht mal die Datei .gitignore an, da seht ihr wie ihr eure
persönlichen Ordner eintragt, die nicht getrackt werden sollen innerhalb des
Repos. Pfadangeben sind relativ zum Ort, wo die .gitignore liegt. Ich hab mir
da also nen eigenen Ordner Philipp gemacht, in dem Bilder für die Modellierung
und solche Sachen liegen.

Branches für eure Features bitte am aktuellsten Knoten von Test_Game aufmachen.
Heißt: pull machen; Test_Game auschecken; letzten Knoten aus Test_Game rechts
klicken; Create Branch here; bennen und Spass haben.

Wichtig: "checkout branchxy" ist eure lokale Version.
         "checkout origin/branchxy" ist die Version unseres Online-Repos

         Wir sollten diese parallel aktuell halten, während wir nicht gerade
         aktiv am Computer sind und an was arbeiten. (Siehe meine Kommentare
         vorhin zum squashing von einzelnen Commits)
--------------------------------------------------------------------------------


--------------------------------------------------------------------------------
Ideen-Sammlung: (vielleicht fächern wirs nicht auf, sondern packen jeweils unseren Namen einfach hinten an)

Vorschlag für Features und Roadmap (Philipp):
1. flache Ebene, Kasten-Tank, eine Sorte Shells (Kreis), one hit-nur ziel treffen, single player, fixed position
2. 2-player, turn-based
3. Health + Damage
4. terrainmodifikation / sound
5. simple Menuführung  (in Abhängigkeit der Möglichkeiten)
6. Level u./o. Speicher-Feature u./o. bewegliche Tanks u./o. verschiedene Shells
   (bzgl. der Shells könnte ich mir für den Anfang was in Richtung
       armor-piercing und high-explosive vorstellen, die Schadensmodifikation in
       Abhängigkeit des Aufschlagsbereiches erhalten)
7. Game-Modes: turn-based multiplayer, real-time 2-player, stop-watch
8. KI


Florian:
Features:
verschiedene arten von fahrzeugen(panzer(schwer, langsam),jeeps(schnell, hält wenig aus), flugzeuge(schwer zu treffen, one-shot-kill), schiffe(sehr begrenzter bewegungsraum(wasser am rand oderso), schwere waffen))
spezialangriffe(externe feuerquellen, starke windbeinflussung)
Wetter(wind, nebel, regen(senkt reichweite), sandsturm(schädigt))
Teams(koop gegen Ki, mehrspieler 2 vs 2 online)
terrain-physik(einsturz wenn unterhölt usw.)
terrainattacken(erdbeben)
3d-grafik(killcams, matchvorschau)
