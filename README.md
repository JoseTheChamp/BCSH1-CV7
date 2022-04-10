# Exercise 07 - Liga Mistrů - zápis a čtení do textového souboru

Pokračujte v projektech z minulého cvičení a nahraďte v knihovně použití pole za ADS spojový seznam.

## Postup
* Projekty (obsah složek) **ChampionsLeague** a **ChampionsLeagueLibrary** nahraďte hotovými projekty z minulého cvičení
* Projekt ChampionsLeagueLibraryTests neměňte! Obsahuje nové jednotkové testy.
* Ve třídě **Player**
  * přetižte metody **Equals** a **GetHashCode**, tak aby porovnávali objekty hráčů podle hodnot vlastností
  * **Equals** vrací **true** pouze v případě, kdy platí ```pl1.Name==pl2.Name && pl1.Club==pl2.Club && pl1.GoalCount==pl2.GoalCount```
  * **GetHashCode** vrací libovolné celé číslo pro objekt hráče
    * Pokud pro dva hráče platí ```pl1.Equals(pl2)```, pak platí ```pl1.GetHashCode()==pl2.GetHashCode()```
* V knihovně vytvořte rozhraní **IPlayersSaveableLoadable** (ns **ChampionsLeagueLibrary**)
  * metoda ```Player[] Save()```
  * metoda ```void Load(Player[] loadedPlayers)```
* Ve třídě **PlayerRecords**
  * implicitně implementujte rozhraní **IPlayersSaveableLoadable**
    * metoda **Save** vrátí pole všech hráčů (velikost pole dle počtu hráčů).
    * metoda **Load** smaže všechny stávající záznamy hráčů a přidá záznamy z pole **loadedPlayers**.
* V knihovně vytvořte třídu **PlayersFileSerializerDeserializer** (ns **ChampionsLeagueLibrary**)
  * atributy:
    * ```readonly IPlayersSaveableLoadable players```
    * ```readonly string file```
  * metody:
    * parametrický konstruktor nastavující oba atributy v pořadí dle definování
    * veřejná metoda ```void Save()```
      * metoda otevře soubor **file** pro zápis
      * každý hráč v **players** (získano voláním **players.Save()**) je zapsán na nový řádek do **file**
      * pro převod hráče na řetězec je využita metoda **Serialize**
      * soubor **file** je uzavřen
    * veřejná metoda ```void Load()```
      * metoda otevře soubor **file** pro čtení
      * jsou načteny všechny řádky textu a převedeny do pole hráčů (s využitím metody **Deserialize**)
      * soubor **file** je uzavřen
      * hráči jsou nahráni do objektu players (**players.Load(...)**)
    * privátní statické metody
      * ```string Serialize(Player p)```
      * ```Player Deserialize(string s)```
      * metody slouží pro převod objektu hráče na string a naopak
      * pro metody platí:
        * pro každého hráče - ```Player a```
        * po provedení - ```Player b = Deserialize(Serialize(a))```
        * musí platit - ```a.Name==b.Name && a.Club==b.Club && a.GoalCount==b.GoalCount```
* Do GUI aplikace vytvořte dvě nová tlačítka **Uložit**, **Načíst**, které zapíšou/načtou hráče s využitím objektu **PlayersFileSerializerDeserializer**
  * Pro výběr souboru využijte **SaveFileDialog** a **OpenFileDialog**
    * Využijte vlastnost **Filter**, aby byly zobrazeny pouze soubory s příponou **.players**
