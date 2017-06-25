SpieleProgrammierungSoSe2017
--------------------------------------------------------------------------------

UPDATE 25.06.2017 (Philipp) CycleSystem + Observer fuer ShellDestruction
--------------------------------------------------------------------------------
Habe grosse updates am CycleSystem vorgenommen, wozu auch Aenderungen an anderer
Stelle noetig waren.
Hauptsaechlich geaendert: TempRoundTimer, ControlCycler

Dazu kommen kleinere Aenderungen an ShellDestruction, die ihr aber zur Kenntnis
nehmen solltet.
Da ich registrieren musste, wann es zu einem Einschlag kommt, gibt es jetzt die
Moeglichkeit sich bei der ShellDestruction als Observer zu registrieren.

Der Observer muss das Interface IDestructionObserver implementieren.
Die Benachrichtigung findet auf CollisionEnter statt.

Neue Features im CycleSystem:
* Timer stoppt nach abgegebenem Schuss bis zum Zeitpunkt des Einschlags
* während die Shell fliegt darf sich der entsprechende Tank noch bewegen
* trifft die Shell auf, zentriert die Cam auf den Einschlagsort
* nach dem Einschlag gibt es 2 Sekunden Verzögerung bis der naechste Spieler
  an der Reihe ist

UPDATE 23.06.2017 (Philipp)
--------------------------------------------------------------------------------
Ich habe heute gelernt: nicht jeder Shader kann per se mit Transparenz umgehen.
Toon-Lit zum Beispiel nicht. Zumindest nicht gut. Man kann immer mal testen
MipMaps auszuschalten und vor allen Dingen auch Alpha is Transparency
aktivieren. Mit dem richtigen Shader haut das dann auch hin mit der Transparenz.
Es gibt sogar nen Standardshader, der dafür da ist.

UPDATE 17.06.2017 (Philipp)
--------------------------------------------------------------------------------
Ich habe einige Dinge überarbeitet/implementiert:
* Input, der NICHT als Achse funktionieren soll, sprich alles, was nicht die
  Links/Rechtsbewegung der Vehicles ist, wird jetzt über ein zentrales
  InputSkript festgelegt, welches die Werte als static strings verwaltet und
  entsprechende Getter-Methoden besitzt; Hintergrund ist bessere Maintainability
  Noch anzupassen wäre demnach das Skript, das den Lane-Wechsel steuert
* Auf Grund der aktuellen Prioritäten habe ich noch nicht den Sprit-Modus
  implementiert
* neu implementiert: ein Schuss pro Runde (das Skript wurde von mir so angelegt,
  dass es möglcihst einfach sein soll später auf mehr Schüsse pro Runde zu
  verallgemeinern)
* das Abfeuern des Schusses leitet eine kleine taktische Rückzugphase von 3
  Sekunden ein, bevor die Runde endet; das bedeutet implizit, dass ich auch
  den Zusammenhang zwischen abgefeuertem Schuss und dem Ende der Runde des
  jeweiligen Spielers implementiert habe.
* die Käthe hat jetzt nen eigenen TODO-Bereich :D
* als ich bissl Zeit hatte und mal was Anderes machen wollte, habe ich ein
  kleines Kommandozeilentool geschrieben, was eine rudimentäre statistische
  Auswertung eines Todo-files nach unserem Standard erstellt und für jede
  Subcategory die noch nicht erledigten Todos auflisten kann :)
  (ich sollte dabei vielleicht anmerken: es ist wunderbar)

BTW: wems noch nicht aufgefallen sein sollte; dieses markdown-file folgt auch
gewissen Formatierungsstandards.
Ein Eintrag besteht aus einer Kopfzeile mit Art des Eintrags, Datum und Namen
des Erstellers in Klammern, darunter eine Zeile mit 80 Bindestrichen. Direkt im
Anschluss folgt die Nachricht, an deren Ende eine Leerzeile einzufügen ist.
Der oberste Eintrag ist immer der jüngste. Hintergrund: Überblick und die
Tatsache, dass das z.B. von GitHub hübsch interpretiert wird.

UPDATE 14.06.2017 (Philipp)
--------------------------------------------------------------------------------
Kurzes Tutorial wie ein Level gestaltet werden muss um nach aktuellem Stand
der Entwicklung lauffähig zu sein bzw. ansatzweise sinnig zu sein.

Ein Level benötigt:
* einen Gamemaster2000
* ein ModeHUD(zur Zeit nur TimeMode verfügbar)
* eine als solche getagte MainCamera
* ein directional light
* ein Terrain(oder eine andere "Spielfläche")
* die Tanks beider spieler
* Skript TempRoundTimer im Gamemaster2000 benötigt eine ref auf den Counter-Text
  (das gilt nur für den TimeMode)
* Skript ControlCycler im Gamemaster2000 braucht cam ref + refs auf alle tanks
* Levelconstraints für die Tankbewegung (ohne lauffähig aber wenig sinnig)
* MainCamera benötigt ein CameraMovement-Skript mit gesetzten Werten

Bei dem ersten Level1 scheint es zu einer Korruption des Zustandes gekommen zu
sein, der ein richtiges Arbeiten des CycleSkripts verhindert. In einer
Neuauflage funktioniert bis jetzt alles nach Plan.

UPDATE 11.06.2017 (Philipp)
--------------------------------------------------------------------------------
Zur Dokumentation eines wichtigen heutigen Lerneffektes:

Wenn ein Objekt zum Zeitpunkt des ersten Importes (der hoffentlich in Unity
auf Basis eines Blender-Exportes nach .fbx geschehen ist) noch keine
UV-Koordinaten besessen hat, ist es moeglich, die mit mittelmäßigem Aufwand
später noch einzupflegen. Der Vorgang ist wie folgt:

1. Erstellen des UV Unwraps in Blender auf dem .blend
2. Erneutes Exportieren des Objektes via Blender nach .fbx
   !!!es ist KEIN erneuter Import in Unity nötig!!!
3. Die fraglichen MESHES des neuen Exportes werden auf die zu ersetzenden
   Meshes gezogen und sind danach mit entsprechenden Texturen belegbar (zb png)

Wichtige Erkenntnis dabei: offensichtlich ist die Information der UV-Koordinaten
im Mesh gespeichert.
Der obige Weg stellt insbesondere sicher, dass nicht zig neue Dateien angelegt
werden müssen und dürfte generell ne gute Möglichkeit sein vorhandene Meshes
per se auszutauschen.

UPDATE 27.05.2017 (Philipp)
--------------------------------------------------------------------------------
Zusammenfassung der heutigen Ergebnisse + Grobeinteilung für die Präsentation:

Zusammen am Anfang:
* relevantes der Gruppe
    * Konzeption (warum dieses Spiel; Skalierbarkeit)
    * Veränderungen: Lane-Idee, Diskussionen über multiplayer
    * Ideen-Sammlung (ungeachtet fehlender Vorkenntnisse)
    * frühe Einigung auf grobe Naming-Conventions
* kurzer Überblick über Dokumentations und Versionierungsstrategie

Florian beginnt:
* haben zunächst mit Movement angefangen, daraus entstand wegen der Lane-Idee
  die Notwendigkeit sich um Schwierigkeiten der Abweichung von Pos und Rot zu
  kümmern; Verweis an Philipp bzgl. des Movements
* HoldLine und Lane-Wechsel
* Erste Idee für Kollisionsabfrage
    * daraus resultierende Problematik
    * daraus entstandene Gegenstrategie
* Shell-Damage

Philipp:
* Movement (kurzer Verweis auf Shell-Rotation und Barrel-Movement)
    * Relativität von Koordinatensystemen
    * Wahl geeigneter Bezugssysteme
    * Testphasen
    * Bremsschwierigkeiten
    * Schwierigkeiten durch Kontrollübergabe (Hinweis auf seltsame Effekte in
      Unity bzgl Aktiviertheit von einzelnen Skripten/Komponenten)
    * Kombination diverser Ansätze
* Modellierung und Animation
    * Normierung
* Timer-Skript und Kontrollwechsel

Christian:
* Overlay


Ausblick:
* Versuch Lane-Wechsel fließend zu machen
* variablere Variante des Kontrollwechsels, da der nur POC ist
* Sprit-Modus als Variante zur Zeitsteuerung soll getestet werden
* Explosionsradien für Shells

UPDATE 26.05.2017 (Philipp)
--------------------------------------------------------------------------------
Bezüglich Beschleunigung, MaxSpeed, Bremsweg steht das Skript zur Bewegung.
Als nächstes mache ich mich daran das Umdrehen auf einer Lane zu skripten.

So wie das aktuelle Skript arbeitet, würde vermutlich der Bremsweg beim Umdrehen
etwas länger als beim stoppen. Ich bin noch nicht sicher, ob ich das am Ende
so haben will. Das müssen wir dann schauen.


UPDATE 23.05.2017 (Philipp)
--------------------------------------------------------------------------------
Bezüglich der Vorträge am Montag wollte ich hier schonmal festhalten, was ich
vorhabe zu machen bzw. worüber mindestens einer von uns reden sollte:

* Shell-Flugbahn und Rotation (insbesondere Fehlschlag des ersten Ansatzes)
* Blender-Objekte und Animation Standartisierung der Models
* Probleme mit der Barrel Bewegung
* WheelCollider (Verwerfung der Zweitidee)
* Timer und Kontrollwechsel
* Verwerfung Echtzeit für den Anfang
* Lane-Idee kam später hinzu
* Dokumentationsphilosophie


UPDATE 21.05.2017 (Philipp)
--------------------------------------------------------------------------------
WICHTIG: ich habe ein bissl was bei den Kontrollskripten gemacht.
Ich glaube meine jetztige Lösung ist um einiges eleganter als die letzte.
Um deren Funktionalität zu wahren ist allerdings eine Sache entscheidend.
Um weniger performante Zugriffe auf die einzelnen steuernden Skripts eines
Fahrzeugs zu vermeiden, habe ich für die Tanks bereits ein Kindobjekt
geschaffen, welches lediglich sämtliche Steuerskripte des Fahrzeugs enthält.
Das hat folgende Vorteile:

* bessere Übersichtlichkeit

* Möglichkeit indexiert zuzugreifen ohne unnötig viel Reflection nutzen zu müssen

* alle Skripte können gleichzeit durch die Deaktivierung des Vaterelementes
 ausgeschaltet werden

 Damit das klappt verwende ich folgende Konvention (das ist das Wichtige):
 Das "Skriptsammelkind" muss das erste Kind in der Hierarchie unterhalb des
 Wurzelknotens des Fahrzeugs sein. Siehe TankBasic-Prefab als Referenz.

UPDATE 19.05.2017 (Philipp)
--------------------------------------------------------------------------------
Timer läuft jetzt; den werden wir wohl für den Rest noch anpassen müssen.
Ich definier schonmal paar Interfaces für die Kommunikation zwischen dem Skript,
was die Kontrolle vergibt und den Fahrzeugen; sprich Sachen in Bezug auf
Schaden und Zerstörbarkeit und son Zeug.

Ich hab übrigens nen Weg finden können per online-Suche, wie der Mesh-Austausch
für mich bissl simpler wird. Wir können uns da also durchaus irgendwann mal
zusammensetzen und schauen, wies uns am besten gefällt.

Wir sollten versuchen nächste Woche bissl mehr abzuarbeiten als diese.

Zur Dokumentation unserer letzten Absprachen, damit wir später weniger Arbeit
haben; wir haben beschlossen, dass:

* wir zunächst auf Beschleunigung verzichten wollen
* wir das Höhenmuster auf allen lanes gleich halten wollen (zumindest vorerst)
* wir als erstes die rundenbasierte Version anfangen wollen, da wenig Zeit ist
 und wir zunächst Tastaturprobleme vermeiden wollen und zu wenig Kapazitätten
 haben, um direkt den Online-Multiplayer anzugehen

--------------------------------------------------------------------------------
UPDATE 12.05.2017: (Philipp)
--------------------------------------------------------------------------------
Anleitung zum todo
Alle Dateien, denen ihr die Dateiendung todo gebt, werden automatisch von dem
Atom-Plugin "tasks" in schöner Weise verarbeitet.
Eine Zeile, die von einem Doppelpunkt beendet wird, gilt als Überschrift.
Diese ist maßgeblich dafür, was beim Abhaken eines Punktes anotiert wird.
Daher stehen auch immer schön eure Namen hinter den Häkchen

* ein todo-Punkt wird durch Strg + Enter hinzugefügt
* ein todo-Punkt wird durch Strg + D abgehakt

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
Ideen-Sammlung: (vielleicht fächern wirs nicht auf, sondern packen jeweils
                 unseren Namen einfach hinten an)

Vorschlag für Features und Roadmap (Philipp):
1. flache Ebene, Kasten-Tank, eine Sorte Shells (Kreis), one hit-nur ziel treffen,
   single player, fixed position
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
verschiedene arten von fahrzeugen(panzer(schwer, langsam),jeeps(schnell,
hält wenig aus), flugzeuge(schwer zu treffen, one-shot-kill), schiffe(sehr
begrenzter bewegungsraum(wasser am rand oderso), schwere waffen))
spezialangriffe(externe feuerquellen, starke windbeinflussung)
Wetter(wind, nebel, regen(senkt reichweite), sandsturm(schädigt))
Teams(koop gegen Ki, mehrspieler 2 vs 2 online)
terrain-physik(einsturz wenn unterhölt usw.)
terrainattacken(erdbeben)
3d-grafik(killcams, matchvorschau)
