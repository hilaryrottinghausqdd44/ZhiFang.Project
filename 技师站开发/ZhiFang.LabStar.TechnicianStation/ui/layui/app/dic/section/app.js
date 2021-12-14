/**
   @Name：检验小组
   @Author：liangyl
   @version 2019-05-30
 */
layui.extend({
	uxutil: 'ux/util',
	uxtable:'ux/table',
	sectionTable:'app/dic/section/list',
	SectionForm:'app/dic/section/sectionForm',
	SectionItemList:'app/dic/section/item/list',
	phrasetab:'app/dic/section/phrase/index',	
	hiscomptable:'app/dic/section/hiscomp/list',
	commonzf:'/modules/common/zf'
}).use(['uxutil','table','form','element','sectionTable','SectionForm','SectionItemList','phrasetab','hiscomptable'],function(){
	var $ = layui.$,
	    element = layui.element,
		form = layui.form,
		uxutil=layui.uxutil,
		sectionTable = layui.sectionTable,
		SectionForm = layui.SectionForm,
		SectionItemList  = layui.SectionItemList,
		phrasetab = layui.phrasetab,
		hiscomptable = layui.hiscomptable,
		table = layui.table;
    //全局变量
    var config = {
    	//检验小组
    	SectionTab:null,
    	//小组项目
    	ItemTab:null,
    	//历史前值对比
    	HistoryTab:null,
    	//常用短语
    	PhraseTab:null,
    	//小组历史对比
    	HiscompTab:null,
    	SectionTabForm:null,
    	PK:null,//小组ID
    	//当前选择行数据
    	checkRowData:[],
    	//当前激活页签,默认表单,自定义变量0-表单，1-小组项目，2-打印格式，3-历史前值对比，4-常用值短语维护
    	currTabIndex:0,
    	//已激活页签，用于判断页签是否已加载
    	isLoadTabArr:[]
    };
    
    //第一个页签
    var oneTab={
    	//第一行第一个项目列表(没有设置采样组的项目信息)
    	SectionTable : function(index){
		    //小组列表功能参数配置
		    var options={
		    	elem:'#grouptable',
		    	id:'grouptable',
		    	title:'检验小组',
		    	height:'full-75',
		    	size: 'sm', //小尺寸的表格
		    	defaultOrderBy: JSON.stringify([{property: 'LBSection_DispOrder',direction: 'ASC'}]),
		    	done: function(res, curr, count) {
	            	//默认选择第一行
					var rowIndex = 0;
		            for (var i = 0; i < res.data.length; i++) {
		                if (res.data[i].LBSection_Id == config.PK) {
		              	   rowIndex=res.data[i].LAY_TABLE_INDEX;
		              	   break;
		                }
		            }
					//默认选择第一行
					doAutoSelect(this,rowIndex);
					if(count==0){
						$('#formType').addClass("layui-hide");
						config.checkRowData=[];
					}
				}
		    };
		    config.SectionTab = sectionTable.render(options);
    	},
    	SectionForm : function(){
    		config.SectionTabForm = SectionForm.render({});
    	}
    };
    
    //第二个页签
    var twoTab={
    	ItemTable:function(height){
    		//小组项目列表功能参数配置
		    var itemObj={
		    	domId: 'SecitonItemList'
		    };	
		    var win = $(window),
			    maxheight = win.height()-$('#memo').height()-115;
			itemObj.height  =  maxheight;  
		    config.ItemTab = SectionItemList.render(itemObj);
    	},
    	 //打开排序窗口
	    openWinForm :function (){
			var win = $(window),
				maxWidth = win.width()-100,
				maxHeight = win.height()-80,
				width = maxWidth > 800 ? maxWidth : 800,
				height = maxHeight > 600 ? maxHeight : 600;
			layer.open({
				title:'小组排序',
				type:2,
				content: 'sort.html',
				maxmin:true,
				toolbar:true,
				resize:true,
				area:[width+'px',height+'px']
			});
		}
    };
    //小组维护联动
    function initGroupListeners(){
		//小组列表行 监听行单击事件
		table.on('row(grouptable)', function(obj){
			config.checkRowData=[];
			config.checkRowData.push(obj.data);
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	        onSearch(config.checkRowData);
		});
	    //表单保存后处理
		layui.onevent("form", "save", function(obj) {
			var formtype = obj.formtype;
			var msg = '';
			if(formtype=='add')msg='新增成功!';
			   else msg='修改成功!';
			config.PK=obj.id;
			layer.msg(msg,{icon:6,time:2000});
			config.SectionTab.loadData({});
		});
		
		//表单按钮删除成功后刷新小组列表
		layui.onevent("groupform", "del", function(obj) {
		    config.SectionTab.loadData({});
		});
	    //编辑
		$('#edit').on('click',function(){
			if(config.checkRowData.length>0){
				var id =config.checkRowData[0].LBSection_Id;
			    config.SectionTabForm.isEdit(id);
			}
		});
		//排序
		$('#btngroupsort').on('click',function(){
			twoTab.openWinForm();
		});
    }
    element.on('tab(tabs)', function(obj){
    	config.currTabIndex=obj.index;
        var isLoad = false;
        //上一个选择的小组
        var groupId = '';
    	//判断当前页签是否已加载过数据
    	for(var i =0;i<config.isLoadTabArr.length;i++){
    		if(config.isLoadTabArr[i].index==obj.index){
    			groupId=config.isLoadTabArr[i].curRowId;
    			isLoad = true;
    			break;
    		}
    	}
    	switch (config.currTabIndex){
    		case 0:  
    		   if(!isLoad){
    			    oneTab.SectionForm();
				    var obj1 = {index:obj.index,curRowId:config.checkRowData[0].LBSection_Id};
    		    	config.isLoadTabArr.push(obj1);
    		    }
		   	    var win = $(window),
		             maxHeight = win.height()-135;
    		   	$('#LBSection').css("height",maxHeight);
    		    //小组表单
    			break;
    		case 1 ://小组项目维护
    		    if(!isLoad){
    		    	twoTab.ItemTable();
				    var obj1 = {index:obj.index,curRowId:config.checkRowData[0].LBSection_Id};
    		    	config.isLoadTabArr.push(obj1);
    		    }
    		    break;
    		case 2 ://小组历史对比
	    		if(!isLoad){
	    			var obj ={
	    				elem:'#hiscomptable',
				    	title:'打印格式列表',
				    	size: 'sm', //小尺寸的表格
				    	height:'full-135',
	    			};
	    		    config.HiscompTab = hiscomptable.render(obj);
	    			var obj1 = {index:obj.index,curRowId:config.checkRowData[0].LBSection_Id};
    		    	config.isLoadTabArr.push(obj1);
	    		}
    		    break;    
    		 case 3 ://常用短语
	    		if(!isLoad){
	    		    config.PhraseTab = phrasetab.render();
	    			var obj1 = {index:obj.index,curRowId:config.checkRowData[0].LBSection_Id};
    		    	config.isLoadTabArr.push(obj1);
	    		}
    		    break;    
    		default:
    			break;
    	}
    	if(groupId!=config.checkRowData[0].LBSection_Id){
    		onSearch();
		}
    });
    //监听折叠
    element.on('collapse(memo)', function(data){
    	var win = $(window),
			maxheight = win.height()-$('#memo').height()-90;
		 $('#SecitonItemList').css('height',maxheight+'px');
    });
     //页签切换查询
    function onSearch(recs){
    	for(var i =0;i<config.isLoadTabArr.length;i++){
    		//当前页签
    		if(config.isLoadTabArr[i].index == config.currTabIndex){
    			config.isLoadTabArr.splice(i, 1); //删除下标为i的元素
    			var obj1 = {index:config.currTabIndex,curRowId:config.checkRowData[0].LBSection_Id};
    	        config.isLoadTabArr.push(obj1);
    		}
    	}
    	//初始化，默认页签为表单页签
    	if(config.isLoadTabArr.length==0){
    		oneTab.SectionForm();
    	    var obj1 = {index:config.currTabIndex,curRowId:config.checkRowData[0].LBSection_Id};
    		config.isLoadTabArr.push(obj1);
    	}
    	switch (config.currTabIndex){
    	    case 0 ://表单
			    config.SectionTabForm.isShow(config.checkRowData[0].LBSection_Id);
    	     break;
    		case 1 ://小组项目维护
    		    setTimeout(function() {
					config.ItemTab.onSearch(config.checkRowData[0].LBSection_Id, config.checkRowData[0].LBSection_CName, config.checkRowData[0].LBSection_ProDLL);
    		    }, 200);
    		    break;
    		case 2 ://历史前值比对
	    		setTimeout(function() {
	    			config.HiscompTab.loadData({},config.checkRowData[0].LBSection_Id,config.checkRowData[0].LBSection_CName);
				}, 200);
    		    break;  
    		case 3 ://短语
	    		setTimeout(function() {
	    			config.PhraseTab.loadData(config.checkRowData[0].LBSection_Id);
				}, 200);
    		    break;    
    		default:
    			break;
    	}
    }
 
    function afterSortUpdate(data){
    	if(!data.success)return;
    	switch (config.currTabIndex){
    		case 0:  
    		    //小组表单
    		    config.SectionTab.loadData({})
    			break;
    		case 1 ://小组项目维护
				config.ItemTab.onSearch(config.checkRowData[0].LBSection_Id, config.checkRowData[0].LBSection_CName, config.checkRowData[0].LBSection_ProDLL);
    		    break;
    		default:
    			break;
    	}
    }
    function afterCopyUpdate(data){
    	if(data.success)config.ItemTab.onSearch(config.checkRowData[0].LBSection_Id,config.checkRowData[0].LBSection_CName);
    }
	 /***默认选择行
	 * @description 默认选中并触发行单击处理 
	 * @param that:当前操作实例对象
	 * @param rowIndex: 指定选中的行
	 * */
	function doAutoSelect (that, rowIndex) {
		var me = this;	
		var data = table.cache[that.instance.key] || [];
		if (!data || data.length <= 0) return;

		rowIndex = rowIndex || 0;
		var filter = that.elem.attr('lay-filter');
		var thatrow = $(that.instance.layBody[0]).find('tr:eq(' + rowIndex + ')');
		var cellTop11 = thatrow.offset().top;
		$(that.instance.layBody[0]).scrollTop(cellTop11 - 160);

		var obj = {
			tr: thatrow,
			data: data[rowIndex] || {},
			del: function() {
				table.cache[that.instance.key][index] = [];
				tr.remove();
				that.instance.scrollPatch();
			},
			updte: {}
		};
		setTimeout(function() {
			layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
		}, 100);
	}
	 // 窗体大小改变时，调整高度显示
	$(window).resize(function() {
		var win = $(window),
		    maxHeight = win.height()-140;
		 //表单高度
	    $('#LBSection').css("height",maxHeight);
	    
	    var secitonitemheight = win.height()-$('#memo').height()-90;
		$('#SecitonItemList').css('height',secitonitemheight+'px');
	});
	
    //初始化
    function init (){
    	var win = $(window),
			maxHeight = win.height()-140;
        //表单高度
	    $('#LBSection').css("height",maxHeight);
	    //检验小组列表初始化
    	oneTab.SectionTable();
	    //监听联动
        initGroupListeners();
    }
    
    window.afterSortUpdate = afterSortUpdate;
    window.afterCopyUpdate = afterCopyUpdate;
    //初始化
    init();
});