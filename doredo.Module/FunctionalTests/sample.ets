;#DropDB dolaEasyTest

#Application dolaWin
#Application dolaWeb

*FillForm
 User Name = Admin
*Action Log In

#IfDef dolaWin

*Action About...
 
*Action OK

#EndIf

#IfDef dolaWeb
*Action Navigation(My Details)
#EndIf
