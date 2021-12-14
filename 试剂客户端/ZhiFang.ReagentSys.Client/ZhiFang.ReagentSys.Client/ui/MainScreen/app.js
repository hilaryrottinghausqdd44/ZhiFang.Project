/**
   @Name：质控物与批号
   @Author：liuyujie
   @version 2021-01-22
 */
layui.extend({
   uxutil: 'ux/util',
    matTable: 'matTable'
}).use(['uxutil', 'table', 'form', 'element', 'matTable'], function () {
    var $ = layui.$,
        element = layui.element,
        form = layui.form,
        uxutil = layui.uxutil,
        matTable = layui.matTable,
        table = layui.table;
	var href = window.document.location.href;
	var pathname = window.document.location.pathname;
	var pos = href.indexOf(pathname);
	var localhostPaht = href.substring(0, pos);
	var projectName = pathname.substring(0, pathname.substr(1).indexOf('/') + 1);
    //全局变量
    var config = {
        //matTable当前选择行数据
        checkRowData: [],
		//matLotTable当前选择行数据
		checkRowData2: [],
		//二级分类
		GoodsClassTypes:[],
		GoodsClassType:null,
		selectUrl:localhostPaht + projectName  + '/ReaManageService.svc/RS_UDTO_SearchReaGoodsQtyWarningInfo?isPlanish=true',
		getGoodsClassTypeUrl:localhostPaht + projectName + '/ReaManageService.svc/RS_UDTO_SearchGoodsClassTypeJoinQtyDtl?isPlanish=true',
	};
	var app={
		time:5,
		limit:20,
		isScroll:true,
		titletxt:"四川大家",
		dataCode:"",
		setIntervalConfig:null,
		index:0,
		openflag:"1",
		cursor:0
	};
    //初始化
    init();
    //初始化
    function init() {
        //表单高度
        $(".tableHeight").css("height", ($(window).height() - 30) + "px");//设置表单容器高度
        $(".fixedBtnBox").css("width", $("#card").css("width"));//设置固定按钮width
        getUrlData();
		getSet();
		getParams();
		if(app.openflag=="1"){
			$('#openflagshow').show();
			$('#qpviewshow').css("display","none");
		}else{
			$('#openflagshow').css("display","none");
			$('#qpviewshow').show();
		}
		config.GoodsClassType=config.GoodsClassTypes[0].ReaGoodsClassVO_CName;
        var options = {
            elem: '#matTable',
            id: 'matTable',
            title: '质控物',
            height: 'full-140',
			GoodsClassType:config.GoodsClassTypes[0].ReaGoodsClassVO_CName,
			limit:app.limit
        };
        matTable.render(options,[]);
        initGroupListeners();
		setItem(true);
		/* if(app.isScroll&&app.isScroll!="false"){
			setItem(true);
			$('#flagshow').show();
		}else{
			clearInterval(app.setIntervalConfig);
			$('#flagshow').css("display","none");
		} */
    }
	//获得参数
	function getParams() {
	    var params = location.search ? location.search.split("?")[1].split("&") : "";
	    if (!params) return;
	    //参数赋值
	    $.each(params, function (j, itemJ) {
	        if ("type".toUpperCase() == itemJ.split("=")[0].toUpperCase()) {
	            app.openflag= decodeURIComponent(itemJ.split("=")[1]);
	            return false;
	        }
	    });
	};
	function getSet(){
		if(localStorage.getItem('limit')&&localStorage.getItem('limit')!="null"){
			app.limit=localStorage.getItem('limit');
		}
		if(localStorage.getItem('time')&&localStorage.getItem('time')!="null"){
			app.time=localStorage.getItem('time');
		}
		if(localStorage.getItem('isScroll')&&localStorage.getItem('isScroll')!="null"){
			app.isScroll=localStorage.getItem('isScroll');
		}
		if(localStorage.getItem('titletxt')&&localStorage.getItem('titletxt')!="null"){
			app.titletxt=localStorage.getItem('titletxt');
		}
		$("#title2").html(app.titletxt);
	}
	function setItem(typeflag){
		var hql="";
		if(typeflag){
			app.index=0;
			app.cursor++;
		}
		var pageCount = 0 ;
		var indexCount=config.GoodsClassTypes.length;
		config.GoodsClassType=config.GoodsClassTypes[app.index].ReaGoodsClassVO_CName;
		hql="&where=reagoods.GoodsClassType='"+config.GoodsClassTypes[app.index].ReaGoodsClassVO_CName+"'";
		var url=config.selectUrl+hql+"&page="+app.cursor+"&limit="+app.limit;
		uxutil.server.ajax({
			 url: url
		}, function (data) {
			if (data.success === true) {
				app.dataCode=data.DataCode;
				var ResultDataValue = $.parseJSON(data.ResultDataValue);
				pageCount=parseInt(ResultDataValue.count/app.limit)+1;
				matTable.searchData(ResultDataValue.list,app.time,app.isScroll,app.limit,app.cursor,config.GoodsClassType);
				if (pageCount === app.cursor||pageCount < app.cursor){
					app.cursor = 0;
					app.index++;
					if (app.index === indexCount){
						app.cursor = 0;
						app.index=0;
					} 
				}
			}
		});
		if(app.isScroll&&app.isScroll!="false"){
			app.setIntervalConfig=setInterval(function(){
				var pageCount = 0 ; 
				var indexCount=config.GoodsClassTypes.length;
				app.cursor++;
				config.GoodsClassType=config.GoodsClassTypes[app.index].ReaGoodsClassVO_CName;
				hql="&where=reagoods.GoodsClassType='"+config.GoodsClassTypes[app.index].ReaGoodsClassVO_CName+"'";
				var url=config.selectUrl+hql+"&page="+app.cursor+"&limit="+app.limit;
				uxutil.server.ajax({
				     url: url
				}, function (data) {
					if (data.success === true) {
						app.dataCode=data.DataCode;
						var ResultDataValue = $.parseJSON(data.ResultDataValue);
						pageCount=parseInt(ResultDataValue.count/app.limit)+1;
						matTable.searchData(ResultDataValue.list,app.time,app.isScroll,app.limit,app.cursor,config.GoodsClassType);
						if (pageCount === app.cursor||pageCount < app.cursor){
							app.cursor = 0;
							app.index++;
							if (app.index === indexCount){
								app.cursor = 0;
								app.index=0;
							} 
						}
					}
				});
				//hql+="&page="+cursor;
				//matTable.onSearch(hql,cursor);
			},app.time*1000);
			$('#flagshow').show();
		}else{
			clearInterval(app.setIntervalConfig);
			$('#flagshow').css("display","none");
		}
	}
	
	function getUrlData(){
		var url=config.getGoodsClassTypeUrl+'&fields=ReaGoodsClassVO_CName,ReaGoodsClassVO_Id&where=(reagoods.Visible=1)';
		$.ajax({
		    url: url,
		    dataType: 'json',
		    type: 'GET',
		    async: false,
		    contentType: 'application/json',//不加这个会出现错误
		    success: function (res) {
		        if (res.success) {
					 var ResultDataValue = res.ResultDataValue ? JSON.parse(res.ResultDataValue) : {};
					var data= ResultDataValue.list;
					config.GoodsClassTypes=[];
					if (data.length > 0) {
						config.GoodsClassTypes=data;
					}
		        }
		    }
		}); 
	}
    //仪器维护联动
    function initGroupListeners() {
        // 窗体大小改变时，调整高度显示
        $(window).resize(function () {
            var width = $(this).width();
            var height = $(this).height();
            //表单高度
            var tabHeight = ($(window).height() - 70) + "px";
            $('.tableHeight').css("height", tabHeight);
            $(".fixedBtnBox").css("width", $("#card").css("width"));//设置固定按钮width
        });
		//列表数据加载完成后
		layui.onevent("matTable", "done", function (obj) {
			$("#datacode").html(" "+config.GoodsClassType+"："+app.dataCode);
		});
		$("#setInt").on('click', function () {
			var val = $("#setInt").text();
			if (val == "停止") {
			    $("#setInt").text("开始");
				clearInterval(app.setIntervalConfig);
				let bodyContainer = $("#matTablepanel").find('.layui-table-body');
				bodyContainer.css('overflow-y', 'scroll');
				matTable.setIsScroll(false);
			} else {
			    $("#setInt").text("停止");
			    setItem(false);
			};
		});
		$("#opennew").on('click', function () {
			window.open(localhostPaht + projectName+"/ui/MainScreen/index.html?type=2");
		});
		//鼠标右键阻止菜单
		document.oncontextmenu = function (event) {
		    var device = layui.device();
		    if (device.ie) {
		        window.event.returnValue = false;
		    } else {
		        window.event.preventDefault();
		    }
		};
		//监听鼠标右击事件
		$(document).mousedown(function (e) {
		   var e = e || window.event;
		   //e.which: 1 = 鼠标左键 left; 2 = 鼠标中键; 3 = 鼠标右键
		   var flag = false;//是否要弹出自定义菜单
		   if (e.which == 3) {
				var reg = /layui-table/g;
		       if(e.target.className.match(reg)){ // 在统计列表点击
		          flag = true;
		          var _x = e.clientX,
		               _y = e.clientY + getScrollTop();
		           $("#menu2").css({ "display": "block", "left": _x + "px", "top": _y + "px" });
		       }
		   }
		    //不需要弹出自定义菜单
		    if (!flag) {//若自定义菜单存在则隐藏
		         if($(e.target).attr("id") == "col"){
		        	var flag = false;
					window.event.preventDefault();
					layer.open({
					    type: 1, skin: 'layui-layer-lan',
						title:"滚动设置",
					    area: ['364px', '80%'],
					    content: $('#allpanel'),
					    success: function (layero, index) {
							$("#timeform").val(app.time);
							$("#limitform").val(app.limit);
							$("#titletxt").val(app.titletxt);
					        if (app.isScroll&&app.isScroll!="false") {
					            $("#scroll").prop('checked',true);
					        	form.render("checkbox");
					        } else {
					            $("#scroll").prop('checked',false);
					        	form.render("checkbox");
					        }
					    }
					});
		        }else if($(e.target).attr("id") == "savecol"){
					  var flag = false;
					  var storeKey = location.pathname + location.hash + 'matTable';
					   var curTableSession = localStorage.getItem(storeKey);
					    localStorage.removeItem(name);
						if(curTableSession&&curTableSession!="null"&&curTableSession!=null){
							localStorage.setItem('manualOpen'+storeKey, curTableSession);
						}else{
							localStorage.setItem('manualOpen'+storeKey, localStorage.getItem('origin' + storeKey));
						}
				}else if($(e.target).attr("id") == "clearcol"){
					  var flag = false;
					 /* var storeKey = location.pathname + location.hash + 'matTable';
					  localStorage.removeItem('manualOpen'+storeKey); */
					  matTable.clearCache();
					  layer.msg('已还原！', {icon: 1, time: 1000});
				}
		        if ($("#menu2").css("display") != "none") $("#menu2").css("display", "none");
		    }
		});
		//确定
		$('#fix').on('click', function () {
			app.limit=$("#limitform").val();
			app.time=$("#timeform").val();
			app.isScroll=$("#scroll").prop('checked');
			app.titletxt=$("#titletxt").val();
			if (app.isScroll&&app.isScroll!="false") {
				$("#setInt").text("停止");
			}else{
				$("#setInt").text("开始");
			}
			localStorage.setItem('limit', app.limit);
			localStorage.setItem('time', app.time);
			localStorage.setItem('isScroll', app.isScroll);
			localStorage.setItem("titletxt",app.titletxt);
			$("#title2").html(app.titletxt);
			clearInterval(app.setIntervalConfig);
			setItem(false);
			layer.closeAll();
			return false;
		});
		$('#limitform').on("input propertychange", function () {
		    var limitform = document.getElementById("limitform").value;
			if(Number(limitform)<1){
				$("#limitform").val(1);
			}
		});
		$('#timeform').on("input propertychange", function () {
		    var timeform = document.getElementById("timeform").value;
			if(Number(timeform)<3){
				$("#timeform").val(3);
			}
		});
		//操作按钮
		$('#colButton').off('click').on('click', function () {
			window.event.preventDefault();
			layer.open({
			    type: 1, skin: 'layui-layer-lan',
				title:"滚动设置",
			    area: ['364px', '80%'],
			    content: $('#allpanel'),
			    success: function (layero, index) {
					$("#timeform").val(app.time);
					$("#limitform").val(app.limit);
					$("#titletxt").val(app.titletxt);
			        if (app.isScroll&&app.isScroll!="false") {
			            $("#scroll").prop('checked',true);
			        	form.render("checkbox");
			        } else {
			            $("#scroll").prop('checked',false);
			        	form.render("checkbox");
			        }
			    }
			});
		});
		//操作按钮
		$('#savecolButton').off('click').on('click', function () {
			var storeKey = location.pathname + location.hash + 'matTable';
			var curTableSession = localStorage.getItem(storeKey);
			localStorage.removeItem(name);
			if(curTableSession&&curTableSession!="null"&&curTableSession!=null){
				localStorage.setItem('manualOpen'+storeKey, curTableSession);
				layer.msg('已保存！', {icon: 1, time: 1000});
			}else{
				localStorage.setItem('manualOpen'+storeKey, localStorage.getItem('origin' + storeKey));
				layer.msg('已保存！', {icon: 1, time: 1000});
			}
		});
		//操作按钮
		$('#clearcolButton').off('click').on('click', function () {
			matTable.clearCache();
			layer.msg('已还原！', {icon: 1, time: 1000});
		});
    }
	//获得滚动条的高度
	function getScrollTop() {
	      var  scrollTop = 0;
	    if (document.documentElement && document.documentElement.scrollTop) {
	        scrollTop = document.documentElement.scrollTop;
	    } else if (document.body) {
	        scrollTop = document.body.scrollTop;
	    }
	    return scrollTop;
	}
});