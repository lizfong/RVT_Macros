public void GEN_ResizeBOMs()
{
	UIApplication UIapp = this.Application;
	UIDocument UIdoc = UIapp.ActiveUIDocument;
	Document Doc = UIdoc.Document;
	
	//list of View Schedules names to get
	List<String> lst_vsNames = new List<String>();
	lst_vsNames.Add("Duct Spool BOM");
	lst_vsNames.Add("Pipe Spool BOM");
	
	//get list of desired view schedules
	List<ViewSchedule> selectedVs = getViewScheduleByName(Doc, lst_vsNames);
	
	//if view schedules are found	
	if (selectedVs.Count > 0)
	{
		//TaskDialog.Show("Success", vs.Definition.GetFieldCount().ToString());
		using (Transaction trans = new Transaction(Doc))
		{
			if (trans.Start("Resize Spool BOMs") == TransactionStatus.Started)
			{
				foreach (ViewSchedule vs in selectedVs)
				{
					for (int i = 0; i < vs.Definition.GetFieldCount(); i++)
					{
						ScheduleField sf = vs.Definition.GetField(i);
						//Duct Spool BOM columns only
						if (vs.Name == "Duct Spool BOM" && sf.ColumnHeading == "Size")
						{
							sf.GridColumnWidth = .11;
						}
						if (vs.Name == "Duct Spool BOM" && sf.ColumnHeading == "Notes")
						{
							sf.GridColumnWidth = .15;
						}
						//All Connector Columns
						if (sf.ColumnHeading == "C1" || sf.ColumnHeading == "C2" ||sf.ColumnHeading == "C3" || sf.ColumnHeading == "End 1" || sf.ColumnHeading == "End 2")
						{
							sf.GridColumnWidth = .15;
						}
						//Pipe Spool BOM columns only
						if (vs.Name == "Pipe Spool BOM" && sf.ColumnHeading == "Item")
						{
							sf.GridColumnWidth = .25;
						}
					}
				}
						
				Doc.Regenerate();
				if (TransactionStatus.Committed != trans.Commit())
				{
					TaskDialog.Show("Failure","Transaction could not be committed");     
				}
			}
		}
	}
	//if view schedules are not found
	else
	{
		TaskDialog.Show("Failure", "Not Found");
	}
}
