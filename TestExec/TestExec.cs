///////////////////////////////////////////////////////////////
// TestExec.cs - Test Requirements for Project #2            //
// Ver 1.2                                                   //
// Application: Demonstration for CSE687-OOD, Project#2      //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    Alienware 17R2, Core-i7, Windows 10          //
// Author:      Young Kyu Kim, Syracuse University           //
//              (315) 870-8906, ykim127@syr.edu              //
// Source:      Jim Fawcett, CST 4-187, Syracuse University  //
//              (315) 443-3948, jfawcett@twcny.rr.com        //
///////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * This package begins the demonstration of meeting requirements.
 * It will demonstrate project requirements 2, 3, 4, 5, 6, 7, 8, and 9.
 */
/*
 * Maintenance:
 * ------------
 * Required Files: 
 *   TestExec.cs,  DBElement.cs, DBEngine, DBExtensions.cs, Display.cs, DBFactory.cs
 *   PersistEngine.cs, QueryEngine.cs, Scheduler.cs, and UtilityExtensions.cs
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver 1.2 :  7 Oct 15
 * - updated Demonstration Requirements for 3, 4, 5, 6, 7, 8, and 9
 * ver 1.1 : 24 Sep 15
 * ver 1.0 : 18 Sep 15
 * - first release
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Project2Starter
{
  class TestExec
  {
        private DBEngine<int, DBElement<int, string>> dbInt = new DBEngine<int, DBElement<int, string>>();
        private DBEngine<string, DBElement<string, List<string>>> dbString = new DBEngine<string, DBElement<string, List<string>>>();
        private DBEngine<string, DBElement<string, string>> dbString2 = new DBEngine<string, DBElement<string, string>>();

        PersistEngine<int, string> persistEngineInt = new PersistEngine<int, string>();
        PersistEngine<string, List<string>> persistEngineString = new PersistEngine<string, List<string>>();

        Scheduler scheduler;

        QueryEngine<int, string> queryEngine = new QueryEngine<int, string>();
        QueryEngine<string, List<string>> queryEngine2 = new QueryEngine<string, List<string>>();

     void TestR2()
    {
        "Demonstrating Requirement #2".title();
      
        //Element with int key, string payload
        DBElement<int, string> elem = new DBElement<int, string>();
        elem.name = "element";
        elem.descr = "test element";
        elem.timeStamp = DateTime.Now;
        elem.children.AddRange(new List<int>{ 1, 2, 3 });
        elem.payload = "elem's payload";
        elem.showElement();
        dbInt.insert(0, elem);

        DBElement<int, string> elem2 = new DBElement<int, string>();
        elem2.name = "element 2";
        elem2.descr = "test element 2";
        elem2.timeStamp = DateTime.Now;
        elem2.children.AddRange(new List<int> { 4, 5, 6 });
        elem2.payload = "elem2's payload";
        elem2.showElement();
        dbInt.insert(1, elem2);

        //Element with int key, list<string> payload
        DBElement<string, List<string>> elem3 = new DBElement<string, List<string>>();
        elem3.name = "elem2";
        elem3.descr = "This element will be removed in TestR3()";
        elem3.children.AddRange(new List<string> { "1", "2", "3" });
        elem3.payload = new List<string> { "Kimchi", "Bulgogi", "Bibimbap" };
        //elem2.showElement();
        dbString.insert("one", elem3);

        //Element with string key, List<string> payload
        DBElement<string, List<string>> elem4 = new DBElement<string, List<string>>();
        elem4.name = "elem4";
        elem4.descr = "string key, List<string> payload";
        elem4.timeStamp = DateTime.Now;
        elem4.children.AddRange(new List<string> { "Soju", "Makgeolli", "Sool"});
        elem4.payload = new List<string> {"Alice", "Jacqueline"};
        dbString.insert("two", elem4);

        bool haha = dbInt.insert(0, elem2);
        dbInt.showDB();

      WriteLine();
    }
    void TestR3()
    {
      "Demonstrating Requirement #3".title();
            Write("\n  ............Before Delete............");

            dbInt.showDB();

            Write("\n\n  ............After Delete (removing key 1)............");

            dbInt.remove(1);
            dbInt.showDB();
            WriteLine();
    }
    void TestR4()
    {
      "Demonstrating Requirement #4".title();
            DBElement<int, string> elem;                   //Is this correct?
            dbInt.getValue(0, out elem);
            
            Write("\n  ........... Before Edit ...........");
            dbInt.showDB();

            Write("\n\n  ............ After Editing Key 0 ...........");
            dbInt.editName<string>(0, "New Edited Name!!");
            dbInt.editDescr<string>(0, "I have been Edited!!!");
            dbInt.editTimeStamp<string>(0, DateTime.Now);
            dbInt.editChildren<string>(0, new List<int>{ -1, -2, -3} );
            elem.payload = "Edited Payload!";
  
            dbInt.showDB();

            WriteLine();
    }
    void TestR5()
    {
      "Demonstrating Requirement #5".title();
            Write("\nPersisting Databases into XML\n");
            DBElement<int, string> elem;
            dbInt.getValue(0, out elem);
            persistEngineInt.persistDB(dbInt);
            persistEngineString.persistDB(dbString);
            persistEngineInt.displayIntXML();
            persistEngineString.displayStrXML();

            //Demonstrate Unpersist operation
            Write("\nUnpersisting XML<Int> DB into dbUnpersist\n");
            persistEngineInt.unpersistDB(dbInt);
            dbInt.showDB();

            Write("\nUnpersisting XML<String> DB into dbString2\n");
            persistEngineString.unpersistDB(dbString);
            dbString.show<string, DBElement<string, List<string>>, List<string>, string>();

            //Insert Elements to prepare for Augmentation
            DBElement<int, string> elemAug = new DBElement<int, string>();
            elemAug.name = "element7";
            elemAug.descr = "extra element for augmentation";
            elemAug.timeStamp = DateTime.Now;
            elemAug.children.AddRange(new List<int> { 9, 13});
            elemAug.payload = "elem7's payload";
            dbInt.insert(7, elemAug);
            DBElement<int, string> elemAug2 = new DBElement<int, string>();
            elemAug2.name = "element7";
            elemAug2.descr = "blahblahblah";
            elemAug2.timeStamp = DateTime.Now;
            elemAug2.children.AddRange(new List<int> { 77, 99 });
            elemAug2.payload = "huehuehuehuehue";
            dbInt.insert(9, elemAug2);
            
            //Demonstrate Database Augmentation
            Write("\n1. Augmenting existing Integer DB from a different XML file \n");
            persistEngineInt.augmentDB(dbInt);
            dbInt.showDB();

            Write("\n2. Augmenting existing String DB from a different XML file \n");
            persistEngineString.augmentDB(dbString);
            dbString.show<string, DBElement<string, List<string>>, List<string>, string>();

            WriteLine();
    }
    void TestR6()
    {
      "Demonstrating Requirement #6".title();
            
            //Initialize Scheduler
            scheduler = new Scheduler();       //initialize scheduler
            persistEngineInt.clearFile<int>();
            scheduler.setTimerInterval(5);     //persist after 5 seconds
            scheduler.setMaxPersist(1);        //persist only once

            //Begin Timer
            Write("\nStart Timer, Persisting after 5 Seconds\n");
            scheduler.enableTimer();           //Start

            scheduler.disableTimer();

            if (scheduler.getIsPersist() == true)
            {
                persistEngineInt.persistDB(dbInt);
            }

            Write("\nPerforming scheduled persist");
            

            WriteLine();
    }
    void TestR7()
    {
      "Demonstrating Requirement #7".title();
            
            Write("\nQuery : value of a specified key");
            queryEngine.searchValue(dbInt, 0);

            Write("\nQuery : children of specified key\n");
            queryEngine.searchChildren(dbInt, 0);

            Write("\nQuery : set of all keys matching a specified pattern which defaults to all keys");
            queryEngine.searchKeyPattern(dbInt, "0");                         //Fails for "?"
            queryEngine2.searchKeyPattern(dbString, "on");

            Write("\nQuery : all keys that contain a specified string in their metadata\n");
            queryEngine.searchMetadata(dbInt, "element7");
            queryEngine2.searchMetadata(dbString, "elem4");
            
            Write("\nQuery : all keys that contain values written within a specifed time-date interval\n");
            DateTime prev = DateTime.Now;
            DateTime next = DateTime.Now;
            TimeSpan ts = new TimeSpan(24, 00, 0);           //10 hours, 30 min, 0 seconds
            prev = prev.Date - ts;
            next = next.Date + ts;

            queryEngine.searchTimeInterval(dbInt, prev, next);

            DateTime next2 = default(DateTime);
            DateTime prev2 = default(DateTime);

            queryEngine.searchTimeInterval(dbInt, prev, next2); //Supply only one end of the interval

            WriteLine();
    }
    void TestR8()
    {
      "Demonstrating Requirement #8".title();

            Write("\nStoring collection of keys from searchQuery into DBFactory; an immutable database\n");
            DBFactory<int, DBElement<int, string>> dbFactory = new DBFactory<int, DBElement<int, string>>(queryEngine.searchMetadata(dbInt, "element7"));

            dbFactory.show<int, DBElement<int, string>, string>();

            WriteLine();
    }
    void TestR9()
    {
            "Demonstrating Requirement #9".title();
            DBEngine<string, DBElement<string, string>> dbDependency = new DBEngine<string, DBElement<string, string>>();
            PersistEngine<string, string> persistEngineDepend = new PersistEngine<string, string>();

            persistEngineDepend.unpersistDB(dbDependency);
            dbDependency.show<string, DBElement<string, string>, string>();
    }

    static void Main(string[] args)
    {
      TestExec exec = new TestExec();
      "Demonstrating Project#2 Requirements".title('=');
      WriteLine();
      exec.TestR2();
      exec.TestR3();
      exec.TestR4();
      exec.TestR5();
      exec.TestR6();
      exec.TestR7();
      exec.TestR8();
      exec.TestR9();
      Write("\n\n");
    }
  }
}
