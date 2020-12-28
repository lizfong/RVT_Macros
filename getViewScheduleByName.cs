public static List<ViewSchedule> getViewScheduleByName(Document doc, List<string> viewNames)
{	
	// Select all the ViewSchedules in the project
	FilteredElementCollector viewSchedules = new FilteredElementCollector(doc).OfClass(typeof(ViewSchedule));
		
	//create a list variable to add the desired view schedules to
	List<ViewSchedule> lst = new List<ViewSchedule>();
	
	//iterate through all the view schedules
	foreach (ViewSchedule v in viewSchedules)
	{
		//if the name is contained in the compare list
		if (viewNames.Contains(v.Name))
		{
			//add it to the list to return
			lst.Add(v);
		}
	}
	//return the list of matching view schedules
	return lst;		
}
