using System;
System.Xml

namespace MyCook
{
    class Program
    {
        public static void Main(string[] args)
        {
            frigo();
        }

        public static void frigo()
        {
            Console.WriteLine("Bienvenu dans ton frigo virtuelle ici tu auras plein a manger mais seulement en virtuelle.\n " +
                "[attention ce jeu ne te nourris pas n'oublie pas de manger dans la VRAIE VIE");
            bool finish = false;
            while (!finish)
            {
                finish = readText();
            }
        }

        public static bool readText()
        {
            string answerC = answer();
            bool finish = whatAnswer(answerC);
            return finish;
        }

        public static string answer()
        {
            Console.WriteLine("Donne nous une commande");
            string answerC = Console.ReadLine();

            //permet de lire un readline et le retourne si il y est détécté une commande.
            //detecte la première serie de string jusqu'à un espace.
            //relance la fonction si ce n'est pas une commande.

            return answerC;
        }

        public static bool whatAnswer(string answer)
        {
            //relis la string et la commande puis en fonction de la commande retourne la fonction qui correspond avec le reste de la string 
        }

        public static bool addIngredient(string answer)
        {
            // add x ingredients if détecte un nombre sinon ajout un seul ingredient (try catch)
        }

        public static bool addRecipes(string answer)
        {
            //add un recette avec le /add_recipes namRecipe x ingredient or /add_recipes namRecipe ingredient avec try catch si il peut int.parse sinon prend un elem de chaque 
        }

        public static bool help(string answer)
        {
            //print all commandes possibles 
        }

        public static bool makeRecipes(string answer)
        {
            //créé la recette en enlevant le nombre d'ingrédient de chaque et ajoutant le plat si assez d'ingrédient sinon print chaques ingrédient manquant et le nombre qu'il faut
        }

        public static bool showIngredient(string answer)
        {
            //montre les ingrédients et le nombre de chaque ingrédient 
        }

        public static bool showRecipes(string answer)
        {
            /*montre le nom de chaque recette de la forme suivante : 
             nomRecette : 
                -X ingredients
                -X ingredients*/
        }

        public static bool showDishes(string answer)
        {
            //montre les différents plats que l'on a 
        }

        public static bool eatDishes()
        {
            //consomme un plat
        }
        


    }
}
