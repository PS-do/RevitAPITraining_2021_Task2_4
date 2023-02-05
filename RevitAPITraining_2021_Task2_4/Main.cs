using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining_2021_Task2_4
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            List<Level> levels = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Levels)
                .WhereElementIsNotElementType()
                .Cast<Level>()
                .ToList();

            string info=null;


            List<Wall> walls = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Walls)
                .WhereElementIsNotElementType()
                .Cast<Wall>()
                .ToList();


            foreach (Level level in levels)
            {
                int walCount = 0;
                foreach (Wall wall in walls)
                {
                    if (wall.LevelId == level.Id)
                    {
                        walCount++;
                    }
                }

                if (walCount > 0)
                {
                    info += "\n" + level.Name + "  :  ";
                    info +=walCount.ToString()+ " стен.";
                }
            }

            TaskDialog.Show("Результат:", info);
            return Result.Succeeded;
        }
    }
}
