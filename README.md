# .NET_Wiki_Navigation-BreadCrumb
(written by Getschonnik)

* Show a path as navigation breadcrumb  

' Add a new module and paste the following code into it
' Add a panel on the form where you want to show the navigation breadcrumb (e.g: panel_breadcrumb_bg)

Use of the module

	navigation.breadcrumb.setBreadcrumbNavigation(My.Application.Info.DirectoryPath & "\data\Documents", panel_breadcrumb_bg, "")
	
The line above will show the complete path as breadcrumb.
			
	navigation.breadcrumb.setBreadcrumbNavigation(My.Application.Info.DirectoryPath & "\data\Documents\Office", panel_breadcrumb_bg, My.Application.Info.DirectoryPath & "\data")
	
The line above cut the last directory and show only the included directory folders
	
A new handler will created for every breadcrumb label. If you click on one of them, the navigation-breadcrumb will 
replace the current path back to the clicked directory-folder label.


------------


Adjustment: navigation > breadcrumb

Private bcn_startPosX As Integer = 10		' the first label (breadcrumb item) on the X-axis
	
Private bcn_startPosY As Integer = 10		' the first label (breadcrumb item) on the Y-axis (the same for all following items)
	
Private bcn_delimiter As String = ">"		' breadcrumb delimiter/seperator
	
Private bcn_delimiterSpace As Integer = 1	' space between the directory-folder-name and the delimiter/seperator
	
