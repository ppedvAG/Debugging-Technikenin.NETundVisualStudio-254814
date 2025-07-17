# Fortgeschrittenes .NET Debugging

Dieses Dokument bietet einen fundierten Überblick zu zentralen Werkzeugen und Konzepten für das Debugging, Reverse Engineering und die Analyse von .NET-Anwendungen.

---

## 1. Reverse Engineering Tools

### ILSpy [auf GitHub](https://github.com/icsharpcode/ILSpy)

**ILSpy** ist ein kostenloser, quelloffener .NET-Decompiler und Assembly-Browser.  
- **Funktionen:**
  - Dekompilierung von .NET-Assemblies (DLL, EXE) in C#-Quellcode und IL-Code
  - Durchsuchen, Navigieren und Exportieren von Typen, Methoden und Ressourcen
  - Projekt-Export: Erzeugt aus einer Assembly ein vollständiges C#-Projekt
  - Unterstützt aktuelle .NET-Versionen, ReadyToRun-Binaries und Plugins
  - Keine Installation nötig, intuitive Bedienung
- **Einsatzgebiete:**
  - Analyse von Third-Party-Bibliotheken ohne Quellcode
  - Fehleranalyse, Sicherheitsüberprüfung, Code-Review
  - Nachvollziehen von API-Implementierungen und internen Abläufen
- **Lizenz:** MIT  


### dnSpy oder dnSpyEx [auf GitHub](https://github.com/JanBN/dnSpyEx)

**dnSpy** ist ein Open-Source-Tool für .NET-Assembly-Analyse, das Dekompilierung, Debugging und Bearbeitung vereint.
- **Funktionen:**
  - Dekompilierung von .NET- und Unity-Assemblies (C#, IL, Ressourcen)
  - Vollwertiger Debugger: Breakpoints, Callstack, Variableninspektion, Multi-Prozess-Debugging, Tracepoints
  - Assembly-Editor: Methoden, Klassen und Metadaten direkt bearbeiten, IL-Editor
  - Hex-Editor für Low-Level-Analysen
  - Export von Assemblies als C#-Projekt
  - Erweiterbar durch Plugins, Skripting mit C# Interactive
- **Einsatzgebiete:**
  - Reverse Engineering und Patchen von .NET-Anwendungen
  - Live-Debugging und Analyse von Assemblies ohne Quellcode
  - Untersuchung und Modifikation von obfuskierten oder geschützten Programmen
- **Hinweis:** Das Projekt ist archiviert, aber weiterhin nutzbar. dnSpyEx ist ein neuerer Fork.
- **Lizenz:** GPLv3

Die Nutzung von Reverse-Engineering-Tools wie ILSpy und dnSpy ist nur im Rahmen der jeweiligen Lizenzbedingungen und gesetzlichen Vorgaben erlaubt!

---

## 2. Symbole und Symbolserver

**Symbole (PDB-Dateien)** liefern Debug-Informationen wie Quellcode-Zuordnungen, Funktionsnamen und Variablen.  
- **Wofür Symbole?**
  - Erlauben Breakpoints, Callstack-Analyse und Variableninspektion im Debugger
  - Unerlässlich für die Analyse von Crash-Dumps und Remote-Debugging
- **Symbolserver:**
  - Zentrale Ablage für PDB-Dateien (öffentlich oder unternehmensintern)
  - Visual Studio und andere Debugger laden Symbole bei Bedarf automatisch nach
  - Microsoft betreibt öffentliche Symbolserver für Windows- und .NET-Komponenten
  - Symbolpfade werden im Debugger oder per Umgebungsvariable (`_NT_SYMBOL_PATH`) konfiguriert
- **Vorteile:**
  - Effizientes Debugging auch ohne lokale Symbole
  - Versionierung und zentrale Verwaltung von Symbolen

---

## 3. [Sysinternals Suite](https://docs.microsoft.com/sysinternals/)

Die **Sysinternals Suite** ist eine Sammlung leistungsfähiger Windows-Tools, die tiefen Einblick in System- und Anwendungsprozesse bieten.

### Wichtige Tools für Debugging & Analyse:

- **Process Explorer:**  
  - Erweiterter Task-Manager, zeigt laufende Prozesse, Handles, DLLs, CPU/GPU-Auslastung
  - Identifiziert, welche Datei/Registry von welchem Prozess geöffnet ist
  - Visualisiert Parent-Child-Beziehungen von Prozessen

- **Process Monitor (ProcMon):**  
  - Echtzeit-Überwachung von Datei-, Registry-, Prozess- und Netzwerkaktivitäten
  - Filterbare, detaillierte Ansicht aller Systemzugriffe
  - Ideal zur Ursachenanalyse bei Fehlern, Performance-Problemen oder Zugriffsverletzungen

- **Weitere nützliche Tools:**
  - **Autoruns:** Startprogramme und Autostart-Einträge analysieren
  - **TCPView:** Netzwerkverbindungen und offene Ports überwachen
  - **Handle:** Offene Handles und Locks auf Dateien/Objekte finden
  - **RAMMap:** Detaillierte Speicherzuordnung und -verbrauch analysieren

- **Einsatzgebiete:**
  - Fehlerdiagnose, Performance-Analyse, Sicherheitsüberprüfung, Monitoring

---

## 4. Memory Profiling

**Memory Profiler** helfen, Speicherverbrauch und Speicherlecks in .NET-Anwendungen zu erkennen und zu analysieren.

- **Typische Funktionen:**
  - Heap-Analyse: Welche Objekte belegen wie viel Speicher?
  - Identifikation von Speicherlecks (z.B. nicht freigegebene Objekte)
  - Analyse von Objekt-Referenzen und Lebenszyklen
  - Snapshots und Vergleich von Speicherzuständen über die Zeit

- **Bekannte Tools:**
  - **dotMemory** (JetBrains)
  - **Redgate ANTS Memory Profiler**
  - **Visual Studio Diagnostic Tools** (ab Professional/Enterprise)
  
- **Einsatzgebiete:**
  - Optimierung des Speicherverbrauchs
  - Vermeidung von Out-of-Memory-Fehlern
  - Analyse von Performance-Problemen durch Speicherbindung

- **In Visual Studio:**
  - Das integrierte **Memory Usage Tool** im Performance Profiler überwacht den Speicherverbrauch in Echtzeit.
  - Du kannst während einer Profiler-Session **Snapshots** erstellen und vergleichen, um Speicherlecks und ineffiziente Speicherverwendung zu erkennen.
  - Das Tool unterstützt .NET-, ASP.NET-, C++- und gemischte Anwendungen.
  - Im **Diagnostic Tools**-Fenster werden Speicherverbrauch und Objektanzahl grafisch dargestellt; du kannst einzelne Typen, Instanzen und Referenzpfade analysieren.
  - Das **.NET Object Allocation Tool** zeigt, wo und wie Objekte im Code alloziert werden.
  - Zugriff über **Debug > Performance Profiler** in Visual Studio.

- **Weitere bekannte Tools:**
  - [dotMemory (JetBrains)](https://www.jetbrains.com/dotmemory/)
  - [Redgate ANTS Memory Profiler](https://www.red-gate.com/products/ants-memory-profiler/)

---

## 5. [SQL Server Profiler](https://docs.microsoft.com/sql/tools/sql-server-profiler/sql-server-profiler)

Der **SQL Server Profiler** ist ein Werkzeug zur Überwachung und Analyse von SQL Server-Datenbankaktivitäten.

- **Funktionen:**
  - Echtzeit-Mitschnitt von Abfragen, Transaktionen, Sperren und Fehlern
  - Filterung nach Benutzer, Datenbank, Anwendung oder Ereignistyp
  - Performance-Analyse und Identifikation von „teuren“ Queries
  - Nachvollziehen von Deadlocks, Timeouts und Verbindungsproblemen

- **Einsatzgebiete:**
  - Fehleranalyse und Debugging von datenbankgestützten Anwendungen
  - Optimierung von SQL-Statements und Datenbank-Design
  - Sicherheitsüberwachung und Auditing
  
- **In SQL Server Management Studio (SSMS):**
  - Über das Menü **Extras > SQL Server Profiler** starten
  - Im Abfrage-Editor per Rechtsklick: **Abfrage in SQL Server Profiler verfolgen**
  - Im Aktivitätsmonitor: Rechtsklick auf einen Prozess → **Ablaufverfolgungsprozess in SQL Server Profiler**
  - Profiler wird als separates Fenster geöffnet und kann parallel zu SSMS genutzt werden.

---

## 6. Tools für Webentwickler

### [Fiddler](https://www.telerik.com/fiddler) 

Ein weit verbreitetes Proxy-Tool zur HTTP/HTTPS-Analyse und Fehlerdiagnose in Webanwendungen.

- **Funktionen:**
  - Mitschnitt und Analyse von HTTP- und HTTPS-Verkehr zwischen Client und Server
  - Manipulation und Wiederholung von Requests (z.B. zum Testen von APIs)
  - Anzeige und Bearbeitung von Headern, Cookies, Body-Daten
  - Automatisches oder manuelles Setzen von Breakpoints im Datenverkehr

- **Einsatzgebiete:**
  - Debugging und Optimierung von Web-APIs und Single-Page-Anwendungen
  - Analyse von Authentifizierungs- und Autorisierungsproblemen
  - Performance- und Sicherheitsüberprüfung von Webanwendungen

### API-Clients 

Tools zum Testen, Entwickeln und Automatisieren von HTTP-APIs

- **Funktionen:**
  - Senden und Wiederholen von HTTP-Requests  
  - Verwaltung von Umgebungen und Variablen  
  - Unterstützung von REST, GraphQL und weiteren Protokollen  
  - Zusammenarbeit im Team (je nach Tool) 
 
- **Einsatzgebiete:**
  - API-Tests, Automatisierung, Dokumentation, Debugging

- **Unterschiede:**  
  - Postman und Insomnia bieten umfangreiche Features und Enterprise-Funktionen  
  - Hoppscotch und ThunderClient punkten mit einfacher Bedienung und geringem Ressourcenverbrauch  

### Weitere nützliche Tools

  - **Browser Developer Tools** (Chrome, Edge, Firefox): Netzwerk-Tab, Performance-Analyse, DOM-Inspektion
  - **ngrok:** Temporäres Exposing von lokalen Webservern ins Internet für Tests und Demos
  - **Wireshark:** Tiefgehende Analyse von Netzwerkpaketen (über HTTP hinaus)

