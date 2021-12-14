/**
	@name：选择小组
	@author：liangyl
	@version 2021-06-02
 */
layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil','form','table'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		table = layui.table,
		form = layui.form;
    //员工id
    var empIDList = parent.getChildrenData().Ids;
    //员工姓名
    var empNameList = parent.getChildrenData().Names;
	//获取小组列表数据
	var GET_SECTION_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true';
	//获取员工共同数据权限小组
	var GET_SECTION_RIGHT_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryCommonSectionRightByEmpID?isPlanish=true';
    //新增员工小组数据权限
	var ADD_URL = uxutil.path.ROOT  + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddEmpSectionDataRight';
    //删除员工小组数据权限
	var DEL_URL = uxutil.path.ROOT  + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelelteEmpSectionDataRight';
    //当前左列表选择行
    var LEFT_SELECT_DATA =[];
    //当前右列表选择行
    var RIGHT_SELECT_DATA =[];
    //是否第一次加载
    var IS_LOAD = true;
    //左列表 原始数据（刷新后最初的数据）
    var OLD_LEFT_DATA = [];
     //是否第一次加载
    var IS_RIGHT_LOAD = true;
    //右列表 原始数据（刷新后最初的数据）
    var OLD_RIGHT_DATA = [];
    
	$('#CName').html(empNameList);
    var div_left_hieght = $('#left_bk').height();

	var win = $(window),
		maxWidth = win.width(),
		maxHeight = win.height();
    var height =maxHeight - 90;
	//左列表实例
	var table_Ind0 = table.render({
		elem: '#lefttable',
		height:height +'px',//'full-75',
		title: '左列表',
		size:'sm',
		initSort:false,
		limit: 5000,
		cols: [[
			{type: 'checkbox',fixed: 'left'},
			{field:'LBSection_Id',title:'LBSection_Id',width:150,sort:true,hide:true},
			{field:'LBSection_CName',title:'小组名称',flex:1,minWidth:150,sort:true}
		]],
		text: {none: '暂无相关数据' },
		loading:true,
		page: false
	});

    table_Ind0.reload({data:[]});
	//右列表实例
	var table_Ind1 = table.render({
		elem: '#righttable',
		height:'full-70',
		title: '右列表实例',
		size:'sm',
		initSort:false,
		cols: [[
		    {type: 'checkbox',fixed: 'left'},
            {field: 'LBSection_Id',width: 60,title: '主键ID',sort: true,hide: true},
            {field:'LBSection_CName', minWidth:150,flex:1, title: '名称', sort: true}
		]],
		loading:true,
	    page: false,
		limit: 500,
		defaultOrderBy: JSON.stringify([{property: 'LBSection_DispOrder',direction: 'ASC'}]),
		text: {none: '暂无相关数据' }
	});
	
	table_Ind1.reload({data:[]});

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
        changeData('left');
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
        load();
	});
	//保存
	$('#save').on('click',function(){
		onSaveClick();
	});
	//查询
	 //监听查询，小组列表
	$('#search').on('click',function(){ 
		rightLoad();
	});
	//右列表 剔除已选左列表数据
    function resultData(data){
		//左列表现有数据
	    var leftData = table.cache['lefttable'];
		var list = [],isExec=true;
		for(var i=0;i<data.length;i++){
			isExec = true;
    		for(var j=0;j<leftData.length;j++){
    		    if(data[i].LBSection_Id == leftData[j].LBSection_Id){
    		    	isExec = false;
    		    	break;
    		    }	
    	    }
    		if(isExec)list.push(data[i]);
        }
    	return list;
	}
		
    //type向那边添加数据
    function changeData(type){
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
        leftLoad(obj.leftData);
    	rightLoad();
    }
   
   //向左列表添加数据
    function addLeft(leftData,rightData){
   	    //获取右列表选择数据
	    if(RIGHT_SELECT_DATA.length==0){
            layer.msg("未选择小组！", { icon: 5, anim: 6 });
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
                    'LBSection_Id':  rightData[i].LBSection_Id,
                    'LBSection_CName':  rightData[i].LBSection_CName
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
                    'LBSection_Id': leftData[i].LBSection_Id,
                    'LBSection_CName': leftData[i].LBSection_CName
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
	function leftLoad(list){
		table_Ind0.reload({
            data: list
       });
	}
	//右列表数据加载
	function rightLoad(){
		var list = OLD_RIGHT_DATA;
		if(list.length>0)list=resultData(list);

		if($('#searchText').val()){
			list = searchList($('#searchText').val(),list);
		}
		table_Ind1.reload({
            data: list
        });
	}
	var saveErrorCount = 0,
		saveCount = 0,
		saveLength = 0;
	 //保存
	function onSaveClick() {
        //返回新增删除数据
		var idslist = getIdList();
		//显示遮罩
		if(idslist.addIds.length==0 && idslist.delIds.length==0)return;
		var indexs=layer.load();
		saveErrorCount = 0;
		saveCount = 0;
		var num1 = 0,num2 = 0;
		if(idslist.addIds.length>0)num1 = 1;
		if(idslist.delIds.length>0)num2 = 1;
		saveLength = num1+num2;
		//新增
		if(idslist.addIds.length>0)addLink(idslist.addIds.join(','));
		//删除
		if(idslist.delIds.length>0)delLinkById(idslist.delIds.join(','));

	}
	//获取新增与删除的数组
	function getIdList(){
    	var entity={},addList=[],delIDList=[];
    	var leftData = table.cache['lefttable'];
    	var oldleft_data = OLD_LEFT_DATA;
    	//新增
    	var addIds=[],del=[];
    	for(var i=0;i<leftData.length; i++ ){
    		var isAdd = true;
    		for(var j=0;j<oldleft_data.length; j++){
    		
    			if(leftData[i].LBSection_Id == oldleft_data[j].LBSection_Id ){
    				isAdd = false;
    				oldleft_data.splice(j,1);
    				break;
    			}
    		}
    		if(isAdd)addIds.push(leftData[i].LBSection_Id);
    	}
    	//删除 
    	var delIds = [];
    	for (var i = 0; i < OLD_LEFT_DATA.length; i++) {
            var delFlag = true;//是删除的
            for (var j = 0; j < leftData.length; j++) {
                if (OLD_LEFT_DATA[i].LBSection_Id == leftData[j].LBSection_Id) {
                    delFlag = false;//存在 不是删除的
                    break;
                }
            }
            if (delFlag)delIds.push(OLD_LEFT_DATA[i].LBSection_Id);
        }
    	return {addIds:addIds,delIds:delIds};
    }
	 /**添加一条关系*/
	function addLink(sectionIDList){
		var me = this;
		var url = ADD_URL + '?empIDList=' + empIDList+'&sectionIDList='+sectionIDList;
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if(data.success) {
				saveCount++;
			}else{
				saveErrorCount++;
			}
			onSaveEnd();
		});
	};
	/**删除一条关系*/
	function delLinkById(sectionIDList){
		var me = this;
		var url = DEL_URL + '?empIDList=' + empIDList+'&sectionIDList='+sectionIDList;
		
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if(data.success) {
				saveCount++;
			}else{
				saveErrorCount++;
			}
			onSaveEnd();
		});
	};
	function onSaveEnd(){
		if (saveCount + saveErrorCount == saveLength) {
			layer.closeAll('loading');//隐藏遮罩层
			if (saveErrorCount == 0){
				layer.msg('保存成功!',{ icon: 6, anim: 0 ,time:2000 });
				var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
	            parent.layer.close(index); //再执行关闭
			}else{
				layer.msg('保存失败!',{ icon: 5, anim: 0 });
			}
		}
	};
	//获取小组数据
	function LBSection(callback){
		var cols = table_Ind1.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_SECTION_LIST_URL+'&fields='+fields.join(',')+'&sort='+table_Ind1.config.defaultOrderBy;
	    loadData(url,function(data){
	    	OLD_RIGHT_DATA = data;
        	callback(OLD_RIGHT_DATA);
	    });
	}
	//获取查询员工共同数据权限小组
	function CommonSectionRight(callback){
		var cols = table_Ind0.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_SECTION_RIGHT_LIST_URL+'&fields='+fields.join(',')+'&empIDList='+empIDList;
	    loadData(url,function(data){
	    	OLD_LEFT_DATA = data;
	    	callback(data);
	    });
	}
	function loadData(url,callback){
		uxutil.server.ajax({
			url:url
		},function(data){
			var list = (data.value ||{}).list || [];
			callback(list);
		});
	}
	/*
	 * 模糊查询一个数组
	 */
    function searchList(str, container) {
	    var newList = [];
	    //新的列表
	    var startChar = str.charAt(0);
	    //开始字符
	    var strLen = str.length;
	    //查找符串的长度
	    for (var i = 0; i < container.length; i++) {
	        var obj = container[i];
	        var isMatch = false;
	        for (var p in obj) {
	            if ( typeof (obj[p]) == "function") {
	                obj[p]();
	            } else {
	                var curItem = "";
	                if(obj[p]!=null){
	                    curItem = obj[p];
	                }
	                for (var j = 0; j < curItem.length; j++) {
	                    if (curItem.charAt(j) == startChar)//如果匹配起始字符,开始查找
	                    {
	                        if (curItem.substring(j).substring(0, strLen) == str)//如果从j开始的字符与str匹配，那ok
	                        {
	                            isMatch = true;
	                            break;
	                        }
	                    }
	                }
	            }
	        }
	        if (isMatch) {
	            newList.push(obj);
	        }
	    }
	    return newList;
	}
    function load(){
    	LBSection(function(data){
        	CommonSectionRight(function(data1){
        		leftLoad(data1);
        		rightLoad();
        	});
        });
    }
	//初始化
	function init(){
		//默认高度加载
		$(".fiexdHeight").css("height", ($(window).height() - 50) + "px");//设置中间容器高度
        load();
	}
	//初始化
	init();
});