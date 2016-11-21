function r(a)
require(a)
echo_("Script '"..a.."' loaded ")
end
function echo_(a)
print(a);
end
function inf(a)
echo_("Info : "..a)
end
function  run(p,arg) 
 --inf("for Core:run two or more process, use '|'\nUse: cmd | taskmgr | mspaint ")
 if arg == nil then 
 Core:run(p,arg);
 echo_("The argument info is not found, Core:run the app as is !!!")
 else   
Core:run_arg(p,arg)
end 
end
 --function label()
-- -- Creates dialog with these three labels
-- dlg = iup.dialog { iup.vbox { lbl, iup.hbox { lbl_explain, lbl_star }; margin="10x10" }
      -- ; title = "IupLabel Exampl1e" }
-- -- -- Shows dialog in the center of the screen 
-- -- dlg:showxy ( iup.CENTER, iup.CENTER )
-- -- if (iup.MainLoopLevel()==0) then
  -- iup.MainLoop()
--end
function kill_task(arg)
Core:run_arg("taskkill  /f /im "..arg.." /t ",'')
 echo_("Process "..arg.." stopped !")
end
function off(t)
 echo_("Set time for shutdown your pc.\nSet time in minutes.")
 if t == nil then
 t = read_();
end
 Core:run_arg("shutdown"," /s /t "..(t*60))
end 
function reboot()
 
 Core:run_arg("shutdown"," /r")
end
function otm()
 Core:run_arg("shutdown"," /a")
end
function read_()
s= io.read()
return s
end
function exit()
kill_task("lua.exe") 
end
r("inc")