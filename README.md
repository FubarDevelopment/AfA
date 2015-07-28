# Absetzung für Abnutzung

Dies ist eine Bibliothek für die Berechnung der Abschreibungs- und Restbuchwerte

# Unterstützte Berechnungsverfahren:

* Linear
* Arithmetisch degressiv
* Arithmetisch progressiv
* Geometrisch degressiv
* Geometrisch progressiv
* Wechsel von degressiver zu linearer Abschreibung
	* An dieser stelle ist auch eine prozentuale Abschreibung sinnvoll

# Tagesgenauigkeit

* 30 Tage pro Monat (360 Tage pro Jahr)
* Genaues Datum (365/366 Tage pro Jahr)

# Runding des AfA-Datums

* Tag-genau
* Anfang des Monats
* Zum nächsten Monat
	* ```< 15```: aktueller Monat
	* ```>= 15```: nächster Monat
* Auf ein halbes Jahr genau
	* ```< 7```: 01. Januar
	* ```>= 7```: 01. Juli
* Anfang des Jahres

# Rundung der Restbuchwerte

* Die Restbuchwerte können auf ```0-n``` Nachkommastellen gerundet werden
* Andere Rundungsverfahren sind implementierbar

# Unterstützte Plattformen

* .NET 4.5
* Windows 8
* Windows Phone 8.1
* Xamarin iOS
* Xamarin Android

# Verfügbarkeit als NuGet-Paket

Es wird intern schon ein NuGet-Paket erstellt, das allerdings noch nicht freigegeben ist, weil diese Bibliothek noch nicht ausreichend getestet worden ist.
