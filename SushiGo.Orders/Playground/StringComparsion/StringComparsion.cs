namespace Playground.StringComparsion;

internal sealed class StringComparsion : IPlayground
{
    public void Run()
    {
        Console.WriteLine("---------------- StringComparsion --------------");
        //StringComparison.CurrentCulture - taka kultura jak na serwerze/kompurze jest ustawion. UWAGA nie zawsze jest to taka sama kultura jak na kompie. moze byc tak ze ktos zoverriduje to np  CultureInfo.CurrentCulture = new CultureInfo("ja-JP", false);
        // No i brany jest pod uwage Cultura ustawiona w watku która tak jak wyzej mowilem mozna napdisac
        //JEZELI sortujemy ma sens korzystanie tylko z CurrentCulture(lub konkretna kultrua ) ze względu na to ze ł i l bedzie posortowane w odpowedniej kolejnosci zgodznie z alfabetem w przeciwieństwi do StringComparions.Ordinal
        string[] houses = { "Starks", "boltons", "starks" };
        //boltons, starks, Starks
        var a = houses.OrderBy(h => h, StringComparer.CurrentCulture);
        //boltons, starks, Starks
        var b = houses.OrderBy(h => h, StringComparer.InvariantCulture);
        //Starks, boltons, starks
        var c = houses.OrderBy(h => h, StringComparer.Ordinal);

        string[] polishChars = [ "łodz","lodz", ];
        //lodz, łodz - Przy założeniu ze CurrentCulture = pl-PL
        var aa = polishChars.OrderBy(h => h, StringComparer.CurrentCulture);
        // //NIE DZIAŁA TO TAK JEDNEK Co DZIWNE vvv MOZE przez to ze kolejnosc numeryczna dla Ordinal/InvarantCulture równiez jest l a potem ł tak jak w Polski alfabecie
        // //łodz, lodz
        // var bb = polishChars.OrderBy(h => h, StringComparer.InvariantCulture);
        // //łodz, lodz
        // var cc = polishChars.OrderBy(h => h, StringComparer.Ordinal);
        
        
        //StringComparison.Ordinal - najszybszy używa numerycznej reperezantacji Unicode znaków do porównywania. Nie jest powiazany z żadnym alfabetem
        
        //StringComparison.InvariantCulture - nie jest żadną konkretną reporezentacja kultury a raczej jest najbardziej standardową kulturą, podobną do US (en-US) ALE nie taka samą
        //USe case to zapis danych do bazy danych w ustadnaryzowanej formie. Chodzi o to zeby w bazie była data w jednym formacie, czy tez liczba po przecinku w tym samym.
        
        const string string1 = "test";
        const string string2 = "Test";
        Console.WriteLine($"Same string but one uppercase: {string1.Equals(string2)}");
        
        Console.WriteLine($"Same string but one uppercase With ignore case: {string1.Equals(string2, StringComparison.OrdinalIgnoreCase)}");

        const string polishString = "łokno";
        const string withoutPolishSign = "łokno";
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        Console.WriteLine($"Matche: {polishString.Equals(withoutPolishSign, StringComparison.CurrentCulture)}");
        
        
        //In this case Applle is first cuz aplphabetically is first, if both hage same char in first position then it is compared second char etc.
        string str1 = "Banana";
        string str2 = "Apple";

        int result = String.Compare(str1, str2);

        if(result < 0)
        {
            Console.WriteLine($"{str1} comes before {str2}");
        }
        else if (result > 0)
        {
            Console.WriteLine($"{str2} comes before {str1}");
        }
        else
        {
            Console.WriteLine("Both strings are the same");
        }
    }
}