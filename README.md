# Program-Testujacy 
Program w przejrzystej oprawie graficznej, służący do wykonywania testów 
  
# O Programie 
  
### Wybór bazy 
  
Program wczytuje bazy danych w formacie (baza) z folderu "Bazy", tworzone za pośrednictwem programu "Generator Pytań". 
  
Program udostępnia wybór z pomiędzy dostępnych kompatybilnych baz w folderze, w przypadku błędu program powinien zwrócić informację. 
  
W ramach informacji o bazach, program wyświetla nazwę bazy, krótki opis, autora, czas egzaminu oraz informację odnoście liczby losowanych pytań. 
  
Podczas wyboru bazy, moduł odczytu losuje bez powtórzeń pytania które zostaną udostępnione użytkownikowi. 
  
### Egzaminowanie 
  
Obecnie program dysponuje możliwością automatycznej zmiany wielkości pytań i odpowiedzi a także suwaka dzięki czemu program nie powinien mieć problemu z większą ilością lub długością odpowiedzi. 
  
Interakcja z użytkownikiem opiera się na chceckbox'ach. Program akceptuje pytania jednokrotnego oraz wielokrotnego wyboru. 
  
Koniec egzaminu następuje po zakończeniu się wyznaczonego czasu lub naciśnięciu przycisku "Zakończ". W przypadku wciśnięcia przycisku zamknięcia programu podczas egzaminu program zapyta czy użytkownik jest tego świadom. 
  
### Wynik 
  
Po zakończeniu egzaminu zostaje wyświetlony ekran informacyjny który informuje użytkownika o ilości uzyskanych punktów oraz ocenie zaliczenia bądź niezaliczenia (50%+). 
  
Użytkownik ma możliwość sprawdzenia swoich odpowiedzi lub ponownego przejścia do wyboru egzaminu. 
  
Podczas sprawdzania własnych odpowiedzi, treść odpowiedzi poprawnych zaznaczana jest na zielono a błędnych na czerwono co sprzyja przyswajaniu wiedzy.
