Core:echo("time in hours");
local time_ = Core:ReadLine();
Core:run_arg("shutdown"," -s -t "..Core:ToInt(time_)*60*60);
 