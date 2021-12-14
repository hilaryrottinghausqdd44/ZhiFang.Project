/**
	@name：选择分发规则项目.txt
	@author：liangyl
	@version 2021-06-02
 */
layui.extend({
	uxutil: 'ux/util',
	tableSelect: '../src/tableSelect/tableSelect'
}).use(['uxutil','form','table','tableSelect'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		table = layui.table,
		tableSelect = layui.tableSelect,
		form = layui.form;
		
	 //外部参数
	var PARAMS = uxutil.params.get(true);
	//规则ID 
	var RULEID = PARAMS.RULEID;
	//规则名称 
	var RULENAME = PARAMS.RULENAME;
	//分发小组 
	var SECTIONID = PARAMS.SECTIONID;
	
	//获取项目列表数据
	var GET_ITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_SearchLBItemByLBReportDateItem?isPlanish=true';
			//获取小组项目列表数据
	var GET_ITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemByHQL?isPlanish=true';
	//获取分发规则项目明细列表数据
	var GET_TRANRULEITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleItemByHQL?isPlanish=true';
		//保存服务
	var SAVE_URL = uxutil.path.ROOT  + '/ServerWCF/LabStarPreService.svc/LS_UDTO_AddDelLBTranRuleItem';
    //当前左列表选择行
    var LEFT_SELECT_DATA =[];
    //当前右列表选择行
    var RIGHT_SELECT_DATA =[];
    //是否第一次加载
    var IS_LOAD = true;
    //左列表 原始数据（刷新后最初的数据）
    var OLD_LEFT_DATA = [];
	//左列表实例
	var table_Ind0 = table.render({
		elem: '#lefttable',
		height:'full-75',
		title: '左列表',
		size:'sm',
		initSort:false,
		limit: 5000,
		cols: [[
			{type: 'checkbox',fixed: 'left'},
			{type: 'numbers',title: '行号',fixed: 'left'},
		    {field:'LBTranRuleItem_Id',title:'ID',width:150,hide:true},
			{field:'LBTranRuleItem_LBItem_Id',title:'项目编号',width:100,hide:true},
			{field:'LBTranRuleItem_LBItem_CName',title:'项目名称',flex:1},
			{field:'LBTranRuleItem_LBItem_EName',title:'英文名称',width:100},
			{field:'LBTranRuleItem_LBItem_UseCode',title:'用户编码',width:100},
			{field:'LBTranRuleItem_LBItem_DispOrder',title:'次序',width:100,hide:true},
			{field:'isAdd', width:60, title: 'isAdd', hide: true}
		]],
		defaultOrderBy: JSON.stringify([{property: 'LBTranRuleItem_LBItem_DispOrder',direction: 'ASC'}]),
		text: {none: '暂无相关数据' },
		loading:true,
		page: false,
		parseData: function(res){ //res 即为原始返回的数据
			if(!res) return;
            var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
			if(IS_LOAD)OLD_LEFT_DATA = data.list || [];
			return {
				"code": res.success ? 0 : 1, //解析接口状态
				"msg": res.ErrorInfo, //解析提示文本
				"count": data.count || 0, //解析数据长度
				"data": data.list || []
			};
		},
		done: function (res, curr, count) {
			if(count>0){
		    }else{
		    }
		}
	});

    table_Ind0.reload({data:[]});
	//右列表实例
	var table_Ind1 = table.render({
		elem: '#righttable',
		height:'full-75',
		title: '右列表实例',
		size:'sm',
		initSort:false,
		cols: [[
		    {type: 'checkbox',fixed: 'left'},
		    {type: 'numbers',title: '行号',fixed: 'left'},
			{field:'LBSectionItem_LBItem_Id',width: 150,title: '项目编号',sort: true,hide:true},
            {field:'LBSectionItem_LBItem_CName', minWidth:150,flex:1, title: '项目名称', sort: true},
		    {field:'LBSectionItem_LBItem_EName', width:150, title: '英文名称', sort: true},
            {field:'LBSectionItem_LBItem_UseCode', width:150, title: '用户编码', sort: true},
            {field:'LBTranRuleItem_Id0', width:150, title: '采样组ID', hide: true}
            
		]],
		limit: 50,
		loading:true,
		page: true,
		defaultOrderBy: JSON.stringify([{property: 'LBItem_DispOrder',direction: 'ASC'}]),
	    parseData: function(res){ //res 即为原始返回的数据
			if(!res) return;
            var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
			var list =[];
			if(data.list && data.list.length>0)list=resultData(data.list);
			return {
				"code": res.success ? 0 : 1, //解析接口状态
				"msg": res.ErrorInfo, //解析提示文本
				"count": data.count || 0, //解析数据长度
				"data": list || []
			};
		},
		text: {none: '暂无相关数据' },
		done: function (res, curr, count) {
			if(count>0){
				
		    }else{
		    }
		}
	});
	
	table_Ind1.reload({data:[]});
	//下拉框 -- icon 前存在icon 则点击该icon 等同于点击input
    $("input.layui-input+.layui-icon").on('click', function () {
        if (!$(this).hasClass("myDate")) {
            $(this).prev('input.layui-input')[0].click();
            return false;//不加的话 不能弹出
        }
    });
    //左列表监听
	//监听行双击事件
    table.on('rowDouble(lefttable)', function (obj) {
    	var tableBox = $(this).parents('.layui-table-box');
    	var index = $(this).attr('data-index');
        var tableDiv = tableBox.find(".layui-table-body.layui-table-main");
    	var checkCell = tableDiv.find("tr[data-index=" + index + "]").find("td div.laytable-cell-checkbox div.layui-form-checkbox I");
	    if (checkCell.length>0)checkCell.click();
        changeData('right');
    });
    //选中状态
    table.on('checkbox(lefttable)', function(obj){
        var leftcheckStatus = table.checkStatus('lefttable');
        LEFT_SELECT_DATA = leftcheckStatus.data;
    });
	    
    //右列表监听
    //监听行双击事件
    table.on('rowDouble(righttable)', function (obj) {
        var tableBox = $(this).parents('.layui-table-box');
    	var index = $(this).attr('data-index');
        var tableDiv = tableBox.find(".layui-table-body.layui-table-main");
    	var checkCell = tableDiv.find("tr[data-index=" + index + "]").find("td div.laytable-cell-checkbox div.layui-form-checkbox I");
	    if (checkCell.length>0)checkCell.click();
        changeData('left',obj);
    });
    //选中状态
    table.on('checkbox(righttable)', function(obj){
        var rightcheckStatus = table.checkStatus('righttable');
        RIGHT_SELECT_DATA = rightcheckStatus.data;
    });
    // 按钮事件
	//向左移动
	$('#add').on('click',function(){
		changeData('left');
	});
	//向右移动
	$('#remove').on('click',function(){
		changeData('right');
	});
	//重置
	$('#reset').on('click',function(){
        loadData0();
        loadData1();
	});
	//保存
	$('#save').on('click',function(){
		onSaveClick();
	});
	//查询
	 //监听查询，小组列表
	$('#search').on('click',function(){ 
		loadData1();
	});
	 //回车事件
    $("#searchText").on('keydown', function (event) {
        if (event.keyCode == 13) {
        	loadData1();
            return false;
        }
    });
	//右列表 剔除已选左列表数据
    function resultData(data){
		//左列表现有数据
	    var leftData = table.cache['lefttable'];
		var list = [],isExec=true;
		for(var i=0;i<data.length;i++){
			isExec = true;
    		for(var j=0;j<leftData.length;j++){
    		    if(data[i].LBSectionItem_LBItem_Id == leftData[j].LBTranRuleItem_LBItem_Id){
    		    	isExec = false;
    		    	break;
    		    }
    	    }
    		if(isExec)list.push(data[i]);
        }
    	return list;
	}
		
    //type向那边添加数据
    function changeData(type,tr){
    	//左列表现有数据
    	var leftData = table.cache['lefttable'];
        var rightData =  table.cache['righttable'];
    	if(type=='left'){//向左移动数据
		   var obj = addLeft(leftData,rightData);
    	}
    	if(type=='right'){//向右移动数据
		   var obj = addRight(leftData,rightData);
    	}
    	
    	if(!obj)return;
    	IS_LOAD=false;
    	table.reload('lefttable', {
            data: obj.leftData
        });
        if(obj.rightData.length == 0 || !tr){
        	loadData1();
        }else{
        	tr.del();
        }
//  	loadData1();
    }
   
   //向左列表添加数据
    function addLeft(leftData,rightData){
   	    //获取右列表选择数据
	    if(RIGHT_SELECT_DATA.length==0){
            layer.msg("未选择项目！", { icon: 5, anim: 6 });
            return;
        }
        var r_d=[]; 
        leftData.reverse()
        var d1 = [];
        for (var i = 0; i < rightData.length; i++) {
            if(rightData[i].LAY_CHECKED && rightData[i].LAY_CHECKED===true){
                delete rightData[i].LAY_CHECKED
                rightData[i].LAY_TABLE_INDEX && delete rightData[i].LAY_TABLE_INDEX
                 var obj = {
                    'LBTranRuleItem_LBItem_Id':  rightData[i].LBSectionItem_LBItem_Id,
                    'LBTranRuleItem_LBItem_CName':  rightData[i].LBSectionItem_LBItem_CName,
                    'LBTranRuleItem_LBItem_EName':  rightData[i].LBSectionItem_LBItem_EName,
                    'LBTranRuleItem_LBItem_UseCode':  rightData[i].LBSectionItem_LBItem_UseCode,
                    'LBTranRuleItem_Id':  rightData[i].LBTranRuleItem_Id0,
                    'isAdd':'1'
                };
                leftData.push(obj)
            }else{
                delete rightData[i].LAY_TABLE_INDEX
                r_d.push(rightData[i])
            }
        }
        rightData = r_d;
        return {leftData:leftData,rightData:rightData};
    }
    
    //向右边列表添加数据
    function addRight(leftData,rightData){
    	//获取右列表选择数据
	    if(LEFT_SELECT_DATA.length==0){
            layer.msg("未选择项目！", { icon: 5, anim: 6 });
            return;
        }
        var l_d=[]; 
        rightData.reverse();
        var d1 = [];
        for (var i = 0; i < leftData.length; i++) {
            if(leftData[i].LAY_CHECKED && leftData[i].LAY_CHECKED===true){
                delete leftData[i].LAY_CHECKED
                leftData[i].LAY_TABLE_INDEX && delete leftData[i].LAY_TABLE_INDEX
                 var obj = {
                    'LBSectionItem_LBItem_Id': leftData[i].LBTranRuleItem_LBItem_Id,
                    'LBSectionItem_LBItem_CName': leftData[i].LBTranRuleItem_LBItem_CName,
                    'LBSectionItem_LBItem_EName': leftData[i].LBTranRuleItem_LBItem_EName,
                    'LBSectionItem_LBItem_UseCode': leftData[i].LBTranRuleItem_LBItem_UseCode,
                    'LBTranRuleItem_Id0' : leftData[i].LBTranRuleItem_Id
                };
                rightData.push(obj)
            }else{
                delete leftData[i].LAY_TABLE_INDEX
                l_d.push(leftData[i])
            }
        }
        leftData = l_d;
        return {leftData:leftData,rightData:rightData};
    }
	
	
	//左列表数据加载
	function loadData0(){
		var cols = table_Ind0.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		
		var url = GET_TRANRULEITEM_LIST_URL+'&fields='+fields;
		var whereObj = {'where':'lbtranruleitem.LBTranRule.Id='+RULEID};
        IS_LOAD=true;
		table_Ind0.reload({
			url:url,
			where:$.extend({},whereObj,{
				fields:fields.join(','),
				sort:table_Ind0.config.defaultOrderBy
			})
		});
	}
	
	//右列表数据加载
	function loadData1(){
		var cols = table_Ind1.config.cols[0],
			whereObj ={},fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_ITEM_LIST_URL;
		var where ="";
		var searchval = document.getElementById("searchText").value;
		var where ='lbsectionitem.LBSection.Id='+SECTIONID;
		if(searchval)where =where+ " and (LBItem.CName like '%"+searchval+"%' or LBItem.EName like '%"+searchval+"%' or LBItem.UseCode like '%"+searchval+"%')"; 
		if(where)url+= '&where='+where;
		table_Ind1.reload({
			url:url,
			where:$.extend({},{},{
				fields:fields.join(',')
//				sort:table_Ind1.config.defaultOrderBy
			})
		});
	}
	 //保存
	function onSaveClick() {
		//新增实体
		var entity = getEntity();
		var params = JSON.stringify(entity);
		//显示遮罩层
		var config = {
			type: "POST",
			url: SAVE_URL,
			data: params
		};
		uxutil.server.ajax(config, function(data) {
			//隐藏遮罩层
			if (data.success) {
				var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
	            parent.layer.close(index); //再执行关闭
	            parent.afterUpdateTranRuleItem();
			} else {
				if(!data.msg)data.msg='保存失败';
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	}
	function getEntity(){
    	var entity={},addList=[],delIDList=[];
    	var leftData = table.cache['lefttable'];
    	//新增
    	for (var i = 0; i < leftData.length; i++) {
             if (leftData[i].isAdd =='1') {
                var DataTimeStamp = [0,0,0,0,0,0,0,0];
                addList.push({
                	LBTranRule: { Id: RULEID, DataTimeStamp: DataTimeStamp },
                    LBItem: { Id: leftData[i].LBTranRuleItem_LBItem_Id, DataTimeStamp: DataTimeStamp }
                });
            }
        }
    	//删除 
    	for (var i = 0; i < OLD_LEFT_DATA.length; i++) {
            var delFlag = true;//是删除的
            for (var j = 0; j < leftData.length; j++) {
                if (OLD_LEFT_DATA[i].LBTranRuleItem_LBItem_Id == leftData[j].LBTranRuleItem_LBItem_Id) {
                    delFlag = false;//存在 不是删除的
                    break;
                }
            }
            if (delFlag)delIDList.push(OLD_LEFT_DATA[i].LBTranRuleItem_Id);
        }
    	delIDList  = delIDList.join();
    	//第二个如果为true新增时 则判断实体是否已经存在 否则不判断
        var isCheckEntityExist = true;
    	entity={addEntityList:addList,isCheckEntityExist:isCheckEntityExist,delIDList:delIDList};
    	return entity;
    }
	//初始化
	function init(){
		$('#CName').text(RULENAME);
		//默认高度加载
		$(".fiexdHeight").css("height", ($(window).height() - 50) + "px");//设置中间容器高度
        loadData0();
        loadData1();
	}
	//初始化
	init();
});