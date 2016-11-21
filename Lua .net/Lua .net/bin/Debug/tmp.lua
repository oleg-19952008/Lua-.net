

-- глобальные переменные для виджетов и настроек программы

-- максимальное число графиков
plots_number = 5

-- виджеты вкладок, где будут размещаться виджеы ввода данных для каждого графика
tabs = {}

-- контейнеры для виджетов
vboxes = {}

-- чекбоксы для выбора того, какие графики строить
checkboxes = {}

-- здесь храним виджеты с текстом данных о точках
coords = {}

-- виджеты подписи для каждого графика
legends = {}

-- виджеты обозначения осей координат
global_legend = {}


-- к величайшему стыду, в Lua нет стандартной функции split
function string:split(sep)
        local sep, fields = sep or ":", {}
        local pattern = string.format("([^%s]+)", sep)
        self:gsub(pattern, function(c) fields[#fields+1] = c end)
        return fields
end


-- функция рисует на плоттере график по указаным точкам
function draw_plot(pwidget, pnum, data)
   x = data[1].value:split(",")
   y = data[2].value:split(",")

   if checkboxes[pnum].value == "OFF" then return end

   if not (#x == #y) or #x == 0 then
	  iup.Message("Ошибка", 
				  "Задано неверное число точек для графика " .. pnum)
      return
   end
   
   iup.PPlotBegin(pwidget, 0)
   iup.PPlotAdd(pwidget, 0, 0)
   for i = 1,#x do
      iup.PPlotAdd(pwidget, x[i], y[i])
   end
   iup.PPlotEnd(pwidget)
end


-- виджет отвечающий за кнопку построения графика
plot_btn = iup.button{ title = "Построить"}

-- колбэк для кнопки "построить график"
function plot_btn:action()

   -- создать виджет графопостроителя
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

   -- этот блок для обхода бага - без него подпись к первому графику отображаться не будет
   iup.PPlotBegin(plot, 0)
   iup.PPlotAdd(plot,0,0)
   plot.DS_LEGEND = ""
   iup.PPlotEnd(plot)

   -- обходим виджеты с данными
   for i = 1, plots_number do
      -- чтобы свеженарисованный графи отобразился с правильной подписью
	  print(legends[i].value)
      plot.DS_LEGEND = legends[i].value
      -- рисуем график
      draw_plot(plot, i, coords[i])
   end


   -- кнопка сохранения графика в картинку на диске
   save_btn = iup.button{ title = "Сохранить" }

   -- теперь создаем само окно, где будет отображаться график
   plot_dg = iup.dialog
   {
      iup.vbox -- это вертикальный сайзер, помести в него графопостроитель и кнопку
      {
	 plot,
	 save_btn
      },
   }

   -- обработчик кнопки сохранения графика
   function save_btn:action()

	  -- создаем диалог выбора имени файла ля сохранения
	  -- в связи с ограничениями библиотеки сохранять можно только в EMF
	  fs_dlg = iup.filedlg{DIALOGTYPE = "SAVE", FILTER = "*.emf" }
	  iup.Popup(fs_dlg)

	  -- если файл выбран
	  if tonumber(fs_dlg.STATUS) >= 0 then

		 -- дописать при необходимости нужное расширение
		 pic = fs_dlg.value
		 if not (string.sub(pic, string.len(pic)-3) == ".emf") then
			pic = pic .. ".emf"
		 end

		 -- создаем псевдо-холст, ассоциированный с файлом
		 tmp_cv = cd.CreateCanvas(cd.EMF, pic .. " 400x300")
		 -- выводим график на холст
		 iup.PPlotPaintTo(plot, tmp_cv)
		 -- сохраняем данные в файл
		 cd.KillCanvas(tmp_cv)
	  end
   end

   -- отображаем диалог с графиком
   plot_dg:showxy(iup.CENTER, iup.CENTER)

   -- запускаем петлю обработки событий для диалога
   if (iup.MainLoopLevel()==0) then
      iup.MainLoop()
   end

end


-- в цикле создаем вкладки, в которых мы будем размещать виджеты 
-- для сбора данных
for i=1,plots_number do

   -- создание текстовых виджетов, куда будут вводиться координаты точек
   coords[i] = {}
   for j = 1,2 do
      coords[i][j] = iup.text 
      {
		 expand="HORIZONTAL",
		 multiline = "YES",
		 VISIBLELINES = 5
      }
   end

   -- виджет для редактирования подписи к графику
   legends[i] = iup.text{ expand = "HORIZONTAL" }

   -- создаем контейнер вкладки и заполняем его элементами
   vboxes[i] = iup.vbox
   {
	  iup.hbox
	  {
		 iup.label { title = "Подпись графика:" },
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

   -- меняем заголовк вкладки
   vboxes[i].tabtitle = "График " .. i

   -- создаем чекбокс, который будет указывать на то, нужно ли строить
   -- график по данным из указанной вкладки
   checkboxes[i] = iup.toggle{ title= "График" .. i, value = "ON" }
end

-- теперь из заполненных нами контейнеров создаем вкладки
tabs = iup.tabs{unpack(vboxes)}


-- создаем текстовые виджеты для редактирования подписей осей
global_legend[1] = iup.text{}
global_legend[2] = iup.text{}

-- создаем фрейм для общих настроек графика
frame = iup.frame
{
   iup.vbox
   {      
      iup.label{
		 title="Использовать данные:", 
		 expand="HORIZONTAL"
			   },
      iup.vbox
      {
		 unpack(checkboxes)
      },
      iup.label{}, -- пустую подпись можно использовать как распорку
      iup.label{title = "Подписи"},
      iup.hbox { iup.label{ title = "Ось X "}, global_legend[1] },
      iup.hbox { iup.label{ title = "Ось Y "}, global_legend[2] },
      iup.label{},
      plot_btn
   };
   expand = "VERTICAL",
}

-- создаем главное окно программы и наносим на него настройки и табы
dg = iup.dialog
{
   iup.hbox
   {
      frame, tabs
   },
   title="Строим график",
   size = "HALF"
}


-- показываем главное окно и запускаем обработку событий
dg:showxy(iup.CENTER, iup.CENTER)
if (iup.MainLoopLevel()==0) then
  iup.MainLoop()
end