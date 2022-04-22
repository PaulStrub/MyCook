using System;
using System.Xml;
using System.IO;
using System.Threading;

namespace MyCook
{
    class Program
    {

        
        public static char[] splitChar = { ' ' };
        public static string[] commandTab = { "/addIngredient", "/addRecipes", "/help", "/makeRecipes", "/showIngredient", "/showRecipes", "/showDishes", "/eatDishes" };
        static XmlDocument doc;
        static string path = "linq";
        static XmlWriterSettings settings = new XmlWriterSettings();
        static XmlWriter writer ;

        public static void Main(string[] args)
        {
            savePath();
           
            frigo();
        }

        static void savePath() //Load the path of the list 
        {
            string codePath = Directory.GetCurrentDirectory();
            string file = Directory.GetParent(Directory.GetParent(Directory.GetParent(codePath).ToString()).ToString()).ToString();
            if (File.Exists(file + "\\List.xml")) //if xml file already exist : launch it 
            {
                

                settings.Indent = true;
                path = file + "\\List.xml";
                doc = new XmlDocument();
                doc.Load(path);
                //Thread.Sleep(5000);
                doc.Save(path);
                return;
            }
            else // else create a new xml file
            {
                doc = new XmlDocument();
                doc.LoadXml("<Frigo><Ingredient></Ingredient><Recette></Recette><Plat></Plat></Frigo>");
                settings.Indent = true;
                path = file + "\\List.xml";
                writer = XmlWriter.Create(path, settings);
                doc.Save(writer);
                writer.Close();
                
                return;
            }
        }

        public static void frigo() //Start prog
        {
            Console.WriteLine("Bienvenu dans ton frigo virtuelle ici tu auras plein a manger mais seulement en virtuelle.\n " +
                "[attention ce jeu ne te nourris pas n'oublie pas de manger dans la VRAIE VIE]\n\n");
            bool notfinish = true;
            while (notfinish)
            {
                notfinish = readText();
            }
        }

        public static bool readText()
        {
            string[] answerC = answer();
            bool finish = whatAnswer(answerC);
            return finish;
        }


        public static string[] answer()
        {
            Console.WriteLine("Donnes nous une commande \n" +
                "   /help pour les infos");
            string answerC = Console.ReadLine();
            string[] answerTab = null;
            answerTab = answerC.Split(splitChar);

            foreach (string element in commandTab)
            {
                if (element == answerTab[0])
                {
                    Console.Clear();
                    return answerTab;
                }
            }
            return answer();
        }

        public static bool whatAnswer(string[] Answer)
        {
            bool retour;
            string command = Answer[0];
            if (command == commandTab[0])
            {
                retour = addIngredient(Answer);
                return retour;
            }
            else if (command == commandTab[1])
            {
                retour = addRecipes(Answer);
                return retour;
            }
            else if (command == commandTab[2] && Answer.Length==1)
            {
                retour = help();
                return retour;
            }
            else if (command == commandTab[3] && Answer.Length==2)
            {
                retour = makeRecipes(Answer);
                return retour;
            }
            else if (command == commandTab[4] && Answer.Length==1)
            {

                retour = showIngredient();
                return retour;
            }
            else if (command == commandTab[5] && Answer.Length==1)
            {
                retour = showRecipes();
                return retour;
            }
            else if (command == commandTab[6] && Answer.Length==1)
            {

                retour = showDishes();
                return retour;
            }
            else if (command == commandTab[7] && Answer.Length==2)
            {
                retour = eatDishes(Answer);
                return retour;
            }
            Console.WriteLine("Commande non valide");
            return true;

            //relis la string et la commande puis en fonction de la commande retourne la fonction qui correspond avec le reste de la string 
        }

        public static bool addIngredient(string[] Answer)
        {
            
                if (Answer.Length == 3 && alreadyExist(Answer[2],"Ingredient"))
                {
                    try
                    {
                        int answerNum = int.Parse(Answer[1]);
                        string nameI = Answer[2];
                        XmlNode nameIngredient = doc.CreateElement(nameI);
                        nameIngredient.InnerText = answerNum.ToString();
                        XmlNode ing = doc.SelectSingleNode(".//Ingredient");
                        ing.AppendChild(nameIngredient);
                        //créer nouvel ingredient avec number == answerNum et nom == ingredient

                    }
                    catch
                    {
                        Console.WriteLine("tu ne nous donnes pas un commande lisible fais /help pour avoir plus d'informations ");
                        Answer = answer();
                        return addIngredient(Answer);
                        //return une nouvelle entrée answe() pour qu'il écrive à nouveau
                    }

                }
            else if (Answer.Length == 3 && !alreadyExist(Answer[2], "Ingredient"))
            {
                int intToAdd = 0;
                try
                {
                    intToAdd = int.Parse(Answer[1]);
                }
                catch
                {
                    Console.WriteLine("tu ne nous donnes pas un commande lisible fais /help pour avoir plus d'informations ");
                    Answer = answer();
                    return addIngredient(Answer);
                    //return une nouvelle entrée answer 
                }
                XmlNode xn = doc.SelectSingleNode("/Frigo/Ingredient/" + Answer[2]);
                xn.InnerText = (int.Parse(xn.InnerText) + intToAdd).ToString();



            }
            else if (Answer.Length == 2 && alreadyExist(Answer[1],"Ingredient"))
                {

                    string nameI = Answer[1];
                    XmlNode nameIngredient = doc.CreateElement(nameI);


                    nameIngredient.InnerText = "1";
                    XmlNode ing = doc.SelectSingleNode(".//Ingredient");
                    ing.AppendChild(nameIngredient);


                    //créer nouvelle ingredient avec number = 1 et ingredient == nom
                }
            else if (Answer.Length == 2 && !alreadyExist(Answer[1], "Ingredient"))
            {
                XmlNode xn = doc.SelectSingleNode("/Frigo/Ingredient/" + Answer[1]);
                xn.InnerText = (int.Parse(xn.InnerText) + 1).ToString();


                //créer nouvelle ingredient avec number = 1 et ingredient == nom
            }
            
            else
                {
                    Console.WriteLine("tu ne nous donnes pas un commande lisible fais /help pour avoir plus d'informations ");
                    Answer = answer();
                    return addIngredient(Answer);
                    //return une nouvelle entrée answer 
                }
                doc.Save(path);
                return true;
                // add x ingredients if détecte un nombre sinon ajout un seul ingredient (try catch)
            
                if (Answer.Length == 3)
                {
                    try
                    {
                        int answerNum = int.Parse(Answer[1]);
                        string nameI = Answer[2];
                        XmlNode nameIngredient = doc.CreateElement(nameI);
                        nameIngredient.InnerText = answerNum.ToString();
                        XmlNode ing = doc.SelectSingleNode(".//Ingredient");
                        ing.AppendChild(nameIngredient);
                        //créer nouvel ingredient avec number == answerNum et nom == ingredient

                    }
                    catch
                    {
                        Console.WriteLine("tu ne nous donnes pas un commande lisible fais /help pour avoir plus d'informations ");
                        Answer = answer();
                        return addIngredient(Answer);
                        //return une nouvelle entrée answe() pour qu'il écrive à nouveau
                    }

                }
                else if (Answer.Length == 2)
                {

                    string nameI = Answer[1];
                    XmlNode nameIngredient = doc.CreateElement(nameI);


                    nameIngredient.InnerText = "1";
                    XmlNode ing = doc.SelectSingleNode(".//Ingredient");
                    ing.AppendChild(nameIngredient);


                    //créer nouvelle ingredient avec number = 1 et ingredient == nom
                }
                else
                {
                    Console.WriteLine("tu ne nous donnes pas un commande lisible fais /help pour avoir plus d'informations ");
                    Answer = answer();
                    return addIngredient(Answer);
                    //return une nouvelle entrée answer 
                }
                doc.Save(path);
                return true;
            
            

        }

        public static bool alreadyExist(string nameToShearch, string inWhatNode)
        {

                XmlNode xn = doc.SelectSingleNode("/Frigo/" + inWhatNode + "/" + nameToShearch);
                if (xn == null)
                    return true;

                return false;
            
        }

        public static bool addRecipes(string[] Answer)
        {

            bool ThereIsNum;

            bool iscomplete = false;
            try
            {
                int isThereNum = int.Parse(Answer[2]);
                ThereIsNum = true;
               
                if (Answer.Length >= 4)
                    iscomplete = true;

            }
            catch
            {
                ThereIsNum = false;
                
                iscomplete = false;
            }
            if (!iscomplete)
            {
                Console.WriteLine("La commande n'est pas entrée complètement ");
                Answer = answer();
                return addIngredient(Answer);
            }

            if (!alreadyExist(Answer[1], "Recette") )
            {
                Console.WriteLine("le nom de recette existe déja");
                Answer = answer();
                return addIngredient(Answer);
            }
            else
            {


                if (ThereIsNum)
                {

                    string nameR = Answer[1];

                    XmlNode nameRecipes = doc.CreateElement(nameR);
                    XmlNode ing = doc.SelectSingleNode(".//Recette");

                    int numIng;
                    for (int i = 2; i < Answer.Length; i = i + 2)
                    {

                        
                        try
                        {
                            numIng = int.Parse(Answer[i]);
                        }
                        catch
                        {
                            Console.WriteLine("tu ne nous donnes pas un commande lisible fais /help pour avoir plus d'informations ");
                            Answer = answer();
                            return addIngredient(Answer);
                        }
                        XmlNode NameIngredient = doc.CreateElement(Answer[i + 1]);
                        NameIngredient.InnerText = Answer[i];
                        nameRecipes.AppendChild(NameIngredient);

                        //Answer[i]=number 
                        //Answer[i+1]=name
                    }
                    ing.AppendChild(nameRecipes);
                }
                else
                {
                    string nameR = Answer[1];

                    XmlNode nameRecipes = doc.CreateElement(nameR);
                    XmlNode ing = doc.SelectSingleNode(".//Recette");
                    int numIng;
                    for (int i = 2; i < Answer.Length; i++)
                    {
                        try
                        {
                            numIng = int.Parse(Answer[i]);
                            Console.WriteLine("tu ne nous donnes pas un commande lisible fais /help pour avoir plus d'informations ");
                            Answer = answer();
                            return addIngredient(Answer);
                        }
                        catch { }
                        XmlNode NameIngredient = doc.CreateElement(Answer[i]);
                        NameIngredient.InnerText = "1";
                        nameRecipes.AppendChild(NameIngredient);
                    }
                    ing.AppendChild(nameRecipes);
                }
                doc.Save(path);
                return true;
                //add un recette avec le /add_recipes namRecipe x ingredient or /add_recipes namRecipe ingredient avec try catch si il peut int.parse sinon prend un elem de chaque 
            }
        }

        public static bool help()
        {
            Console.WriteLine("Voici les commandes possibles : \n" +
                "    /addIngredient nomIngredient : ajoute un ingredient \n" +
                "    /addIngredient nombreIngredient nomIngredient : ajoute un certain nombre de l'ingredient \n" +
                "    /addRecipes nomRecette nomIngredient1 nomIngredient2 etc : créer une recette qui utilise une fois chaque ingrédients \n" +
                "    /addRecipes nomRecette nombreIngredient1 nomIngredient1 nombreIngredient2 nomIngredient2 etc : créer une recette qui utilise une fois chaque ingrédients \n" +
                "    /help : t'es en train de le faire petit géni \n" +
                "    /makeRecipes nomRecette : créer la recette et ajoute le plat \n" +
                "    /showIngredient : montre les ingrédients et leur nombre \n" +
                "    /showRecipes : montre les recettes \n" +
                "    /showDishes : montre les plats \n" +
                "    /eatDishes nomPlat : consomme un plat \n"
                );
            return true;
        }

        public static bool makeRecipes(string[] Answer)
        {
            if (!alreadyExist(Answer[1], "Recette"))
            {
                XmlNode recipe = doc.SelectSingleNode("/Frigo/Recette/" + Answer[1]);
                XmlNodeList ingredientToUse = recipe.ChildNodes;
                XmlNode ingredient = doc.SelectSingleNode("/Frigo/Ingredient");
                
                int howMuchIng = ingredientToUse.Count;
                int count = 0;
                foreach(XmlNode nodeIn in ingredientToUse)
                {
                    if (!alreadyExist(nodeIn.Name, "Ingredient")) 
                    {
                        if (int.Parse(ingredient[nodeIn.Name].InnerText)>= int.Parse(nodeIn.InnerText))
                        {
                            count++;
                        }
                    }
                }
                if (count == howMuchIng)
                {
                    foreach(XmlNode nodeIn in ingredientToUse)
                    {
                        ingredient[nodeIn.Name].InnerText = (int.Parse(ingredient[nodeIn.Name].InnerText) - int.Parse(nodeIn.InnerText)).ToString();

                    }
                    if (!alreadyExist(recipe.Name, "Plat"))
                    {
                        XmlNode Dishes = doc.SelectSingleNode("/Frigo/Plat/" + recipe.Name);
                        Dishes.InnerText = (int.Parse(Dishes.InnerText) + 1).ToString();
                    }
                    else
                    {
                        XmlNode Dishes = doc.CreateElement(recipe.Name);
                        Dishes.InnerText = "1";
                        doc.SelectSingleNode("/Frigo/Plat").AppendChild(Dishes);
                        doc.Save(path);
                    }
                    
                }
                else
                {
                    Console.WriteLine("Il vous manque des ingrédients pour cette recette");
                }
            }
            else
            {
                Console.WriteLine("la recette n'existe pas");
            }
            
            

            return true;
        }

        public static bool showIngredient()
        {
            XmlNodeList listxml = doc.SelectSingleNode("/Frigo/Ingredient").ChildNodes;
            Console.WriteLine("Vos Ingredients sont : \n");
            foreach(XmlNode node in listxml)
            {
                if (int.Parse(node.InnerText) > 0)
                {
                    Console.WriteLine(" " + node.InnerText + " " + node.Name + " ;");
                }
            }
            Console.WriteLine("\n");
            return true;
            
        }

        public static bool showRecipes()
        {
            XmlNodeList listxml = doc.SelectSingleNode("/Frigo/Recette").ChildNodes;
            Console.WriteLine("Vos Recettes sont : \n");
            foreach (XmlNode node in listxml)
            {
                Console.WriteLine("Pour faire " + node.Name + " il vous faut :");
                foreach(XmlNode nodeIn in node)
                {
                    Console.WriteLine(" " + nodeIn.InnerText + " " + nodeIn.Name + " ;");
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n");
            return true;


        }
         
        public static bool showDishes()
        {
            XmlNodeList listxml = doc.SelectSingleNode("/Frigo/Plat").ChildNodes;
            Console.WriteLine("Vos Plats sont : \n");
            foreach (XmlNode node in listxml)
            {
                if (int.Parse(node.InnerText) > 0)
                    Console.WriteLine(" " + node.InnerText + " " + node.Name + " ;");
            }
            Console.WriteLine("\n");
            return true;
        }

        public static bool eatDishes(string[] Answer )
        {
            if (!alreadyExist(Answer[1], "Plat"))
            {
                XmlNode node = doc.SelectSingleNode("/Frigo/Plat/" + Answer[1]);
                if (int.Parse(node.InnerText) > 0)
                {
                    Console.WriteLine("Miam Miam un régale");
                    node.InnerText = (int.Parse(node.InnerText) - 1).ToString();
                    doc.Save(path);
                }
                else
                    Console.WriteLine("Vous n'avez pas ce plat en stock");
                return true;
            }
            else
            {
                Console.WriteLine("le plat n'est pas disponible ou la commande est mal écrite");
                Answer = answer();
                return addIngredient(Answer);
            }
        }

    }
}
