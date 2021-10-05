# PV179-project
## Tým:
- Kis Lázár Bence
- Xuan Linh Phamová
- Matěj Tužil

**Aplikace pro pěstitele kaktusů. Aplikace se bude skládat ze dvou hlavních částí – správy vlastní sbírky kaktusů a obchodování s kaktusy.**

## Uživatelé
- autentizace uživatelů
- změna osobních údajů (adresa, kontaktní údaje - email, telefon, atd…)
- nahrání profilové fotky
- zobrazení historie provedených obchodů s ostatními uživateli
- zobrazení svých aktuálních nabídek na ostatní kaktusy
- hodnocení uživatele od ostatních z recenzí
- běžný uživatel / správce
- role správce: ověřuje nové druhy, může banovat scamery

## Vyhledávání
- vyhledávání kaktusů podle:
    - pouze ty na prodej / všechny - filter
    - jména
    - druh?
    - ceny

## Custom měna KaktusCoin
- v aplikaci se neplatí penězi, ale custom měnou
- měna lze získat za:
    - prodání kaktusů
    - získání achievementu
    - odsouhlasení přidání nového druhu
    - …
## Správa vlastní sbírky kaktusů
- přidání nového kaktusu do sbírky 
    - výběr druhu 
        - pokud druh neexistuje, musí prvně poslat žádost o přidání nového druhu (je třeba doložení, že ten druh je validní (například nahráním certifikátu, fotky, …))
    - velikost květináče
    - stáří (datum výsevu)
    - fotky
    - počet kusů
    - zda je kaktus na prodej či ne
    - (možnost nechat druh kaktusu a přidat další várku s rozdílným stářím kaktusů)
- Sbírka je rozdělená na dva typy kaktusů - na prodej a pouze na výstavu. Zárověň jde kaktus ve sbírce pro ostatní skrýt
- Ve své sbírce lze vyhledávat, měnit počty kaktusů, mazat, upravovat


## Obchodování s kaktusy
- uživatel může vystavit svůj kaktus/kaktusy na prodej. Určí cenu (v custom měně), za kterou lze kaktus ihned koupit a může slovně popsat svoji nabídku (může zde navrhnout výměnu za jiné kaktusy)
- kupující můžou tvořit nabídku z kombinace custom měny a libovolného množství jiných kaktusů
- prodávající pak dostává nabídky, které může buď přijmout, odmítnout nebo counterovat svou vlastní nabídkou
- k prodeji určitého kaktusu se také mohou přidávat komentáře
- Po provedeném obchodu lze uživatele ohodnotit (recenze) - 0-5 hvězdiček + slovní hodnocení


## Achievementy
- Za achievementy se dostává custom měna
- jsou zobrazené u profilu uživatele
- např. První obchod, 10, 20 obchodů, 100, 1000 druhů kaktusů ve sbírce, dobré hodnocení po více obchodech, odměna za vytvoření nového druhu

---

## User stories
- As a cactus owner, I want to create a profile with my plants, so that I could keep track of my collection.
- As a cactus fan, I want to be able to suggest new cactus species not existing in the system yet, so that they could be appoved and everyone could add their instances of the given species to their collection.
- As a cactus fan, I want to see other people's profiles, so that I could see what species are the most popular and the rarest.
- As a cactus collector, I want to have an option for selling and exchanging my plants, so that I could replace plants I don't need anymore with new species.
- As a user, I want to be able to customize my profile and set a profile picture, so that the site would feel homelike.
- As a user, I want to see the history of all transfers and current account balance, so that I could keep track of my in-app finances.
- As someone who has just made a purchase, I want to have an option for rating the other party, so that I could report potential fraud or make a lasting visible sign of my appreciation.
- As the system admin, I want to be able to ban users who don't follow the rules, so that the usage of the site would provide a pleasantful experience to everyone else.
