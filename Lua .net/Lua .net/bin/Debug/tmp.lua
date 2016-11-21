

-- ���������� ���������� ��� �������� � �������� ���������

-- ������������ ����� ��������
plots_number = 5

-- ������� �������, ��� ����� ����������� ������ ����� ������ ��� ������� �������
tabs = {}

-- ���������� ��� ��������
vboxes = {}

-- �������� ��� ������ ����, ����� ������� �������
checkboxes = {}

-- ����� ������ ������� � ������� ������ � ������
coords = {}

-- ������� ������� ��� ������� �������
legends = {}

-- ������� ����������� ���� ���������
global_legend = {}


-- � ����������� �����, � Lua ��� ����������� ������� split
function string:split(sep)
        local sep, fields = sep or ":", {}
        local pattern = string.format("([^%s]+)", sep)
        self:gsub(pattern, function(c) fields[#fields+1] = c end)
        return fields
end


-- ������� ������ �� �������� ������ �� �������� ������
function draw_plot(pwidget, pnum, data)
   x = data[1].value:split(",")
   y = data[2].value:split(",")

   if checkboxes[pnum].value == "OFF" then return end

   if not (#x == #y) or #x == 0 then
	  iup.Message("������", 
				  "������ �������� ����� ����� ��� ������� " .. pnum)
      return
   end
   
   iup.PPlotBegin(pwidget, 0)
   iup.PPlotAdd(pwidget, 0, 0)
   for i = 1,#x do
      iup.PPlotAdd(pwidget, x[i], y[i])
   end
   iup.PPlotEnd(pwidget)
end


-- ������ ���������� �� ������ ���������� �������
plot_btn = iup.button{ title = "���������"}

-- ������ ��� ������ "��������� ������"
function plot_btn:action()

   -- ������� ������ ����������������
   plot = iup.pplot
   {
      expand="YES",
      TITLE = "Simple Line",
      MARGINBOTTOM="65",
      MARGINLEFT="65",
      AXS_XLABEL = global_legend[1].value,
      AXS_YLABEL = global_legend[2].value,
      LEGENDSHOW="YES",
      LEGENDPOS="TOPLEFT",
      size = "400x300"
   }

   -- ���� ���� ��� ������ ���� - ��� ���� ������� � ������� ������� ������������ �� �����
   iup.PPlotBegin(plot, 0)
   iup.PPlotAdd(plot,0,0)
   plot.DS_LEGEND = ""
   iup.PPlotEnd(plot)

   -- ������� ������� � �������
   for i = 1, plots_number do
      -- ����� ����������������� ����� ����������� � ���������� ��������
	  print(legends[i].value)
      plot.DS_LEGEND = legends[i].value
      -- ������ ������
      draw_plot(plot, i, coords[i])
   end


   -- ������ ���������� ������� � �������� �� �����
   save_btn = iup.button{ title = "���������" }

   -- ������ ������� ���� ����, ��� ����� ������������ ������
   plot_dg = iup.dialog
   {
      iup.vbox -- ��� ������������ ������, ������� � ���� ���������������� � ������
      {
	 plot,
	 save_btn
      },
   }

   -- ���������� ������ ���������� �������
   function save_btn:action()

	  -- ������� ������ ������ ����� ����� �� ����������
	  -- � ����� � ������������� ���������� ��������� ����� ������ � EMF
	  fs_dlg = iup.filedlg{DIALOGTYPE = "SAVE", FILTER = "*.emf" }
	  iup.Popup(fs_dlg)

	  -- ���� ���� ������
	  if tonumber(fs_dlg.STATUS) >= 0 then

		 -- �������� ��� ������������� ������ ����������
		 pic = fs_dlg.value
		 if not (string.sub(pic, string.len(pic)-3) == ".emf") then
			pic = pic .. ".emf"
		 end

		 -- ������� ������-�����, ��������������� � ������
		 tmp_cv = cd.CreateCanvas(cd.EMF, pic .. " 400x300")
		 -- ������� ������ �� �����
		 iup.PPlotPaintTo(plot, tmp_cv)
		 -- ��������� ������ � ����
		 cd.KillCanvas(tmp_cv)
	  end
   end

   -- ���������� ������ � ��������
   plot_dg:showxy(iup.CENTER, iup.CENTER)

   -- ��������� ����� ��������� ������� ��� �������
   if (iup.MainLoopLevel()==0) then
      iup.MainLoop()
   end

end


-- � ����� ������� �������, � ������� �� ����� ��������� ������� 
-- ��� ����� ������
for i=1,plots_number do

   -- �������� ��������� ��������, ���� ����� ��������� ���������� �����
   coords[i] = {}
   for j = 1,2 do
      coords[i][j] = iup.text 
      {
		 expand="HORIZONTAL",
		 multiline = "YES",
		 VISIBLELINES = 5
      }
   end

   -- ������ ��� �������������� ������� � �������
   legends[i] = iup.text{ expand = "HORIZONTAL" }

   -- ������� ��������� ������� � ��������� ��� ����������
   vboxes[i] = iup.vbox
   {
	  iup.hbox
	  {
		 iup.label { title = "������� �������:" },
		 legends[i]
	  },
	  iup.hbox 
	  {
		 iup.label
		 {
			title="X : ", 
		 },
		 coords[i][1]
	  },
	  iup.hbox 
	  {
		 iup.label
		 {
			title="Y : ", 
		 },
		 coords[i][2]
	  };
	  expand="YES",

   }

   -- ������ �������� �������
   vboxes[i].tabtitle = "������ " .. i

   -- ������� �������, ������� ����� ��������� �� ��, ����� �� �������
   -- ������ �� ������ �� ��������� �������
   checkboxes[i] = iup.toggle{ title= "������" .. i, value = "ON" }
end

-- ������ �� ����������� ���� ����������� ������� �������
tabs = iup.tabs{unpack(vboxes)}


-- ������� ��������� ������� ��� �������������� �������� ����
global_legend[1] = iup.text{}
global_legend[2] = iup.text{}

-- ������� ����� ��� ����� �������� �������
frame = iup.frame
{
   iup.vbox
   {      
      iup.label{
		 title="������������ ������:", 
		 expand="HORIZONTAL"
			   },
      iup.vbox
      {
		 unpack(checkboxes)
      },
      iup.label{}, -- ������ ������� ����� ������������ ��� ��������
      iup.label{title = "�������"},
      iup.hbox { iup.label{ title = "��� X "}, global_legend[1] },
      iup.hbox { iup.label{ title = "��� Y "}, global_legend[2] },
      iup.label{},
      plot_btn
   };
   expand = "VERTICAL",
}

-- ������� ������� ���� ��������� � ������� �� ���� ��������� � ����
dg = iup.dialog
{
   iup.hbox
   {
      frame, tabs
   },
   title="������ ������",
   size = "HALF"
}


-- ���������� ������� ���� � ��������� ��������� �������
dg:showxy(iup.CENTER, iup.CENTER)
if (iup.MainLoopLevel()==0) then
  iup.MainLoop()
end