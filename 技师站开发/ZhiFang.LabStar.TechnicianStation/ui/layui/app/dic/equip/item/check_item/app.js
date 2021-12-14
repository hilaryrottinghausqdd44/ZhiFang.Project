/**
   @Name：仪器项目选择
   @Author：zhangda
   @version 2019-08-20
 */
layui.extend({
	uxutil: 'ux/util',
	leftTable:'app/dic/equip/item/check_item/leftTable',
	rightTable:'app/dic/equip/item/check_item/rightTable'
}).use(['uxutil','table','leftTable','rightTable'],function(){
	var $ = layui.$,
		uxutil=layui.uxutil,
		leftTable = layui.leftTable,
		rightTable = layui.rightTable,
		table = layui.table;
	//自定义变量
	var config={
		checkRowData:[],//选中行
		//仪器项目保存服务
        addUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddDelLBEquipItem',
		//左列表选择项
		leftData:[],
		//右列表选择项
		rightData:[],
		//左列表原始数据，用于保存时判断
		oldLeftData:[],
		//左列表原始数据，用于保存时判断
		oldRightData:[]
    };
    var paramsObj = {
        equipID: null,
        equipCName:null,
        sectionID:null
    };
    //获得参数
    getParams();
    //小组列表功能参数配置
    var options={
    	elem:'#leftTable',
    	id:'leftTable',
    	title:'仪器项目',
    	height:'full-100',
        defaultParams: { equipID: paramsObj.equipID},
    	done: function(res, curr, count) {
            rightTable.leftData = res.data;
            if (leftTable.config.positionId) {
                var flag = false;
                var index = null;
                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i]["LBEquipItemVO_LBItem_Id"] == leftTable.config.positionId) {
                        flag = true;
                        index = i + 1;
                    }
                }
                if (flag) {
                    $("#leftTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").addClass('layui-table-click').siblings().removeClass('layui-table-click');
                    document.querySelector("#leftTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").scrollIntoView(false, { behavior: "smooth" });
                }
                leftTable.config.positionId = null;
            } else {
                $("#leftTable+div .layui-table-body table.layui-table tbody tr:first-child").addClass('layui-table-click').siblings().removeClass('layui-table-click');//选中第一条
            }
            layui.event("leftTable", "done", { res: res, count: count });
        }
    };
    var tableIns=leftTable.render(options);
    var data = tableIns.config.data; //编辑后的全部数据
    
    //项目列表功能参数配置
    var options1={
    	elem:'#rightTable',
    	id:'rightTable',
    	title:'项目列表',
        height: 'full-104',
        defaultParams: { equipID: paramsObj.equipID, sectionID: paramsObj.sectionID }
    };
    rightTable.render(options1);
    //获得参数
    function getParams() {
        var params = location.search.split("?")[1].split("&");
        //参数赋值
        for (var j in paramsObj) {
            for (var i = 0; i < params.length; i++) {
                if (j.toUpperCase() == params[i].split("=")[0].toUpperCase()) {
                    paramsObj[j] = decodeURIComponent(params[i].split("=")[1]);
                }
            }
        }
    }
    //设置中间容器高度
    $(".fiexdHeight").css("height", ($(window).height() - 50) + "px");
    // 窗体大小改变时，调整高度显示
	$(window).resize(function() {
		var width = $(this).width();
		var height = $(this).height();
		 //表单高度
		var height = $(document).height() - $(".fiexdHeight").offset().top-50;
	    $('#fiexdHeight').css("height",height);
	});
	//向左移动
	$('#add').on('click',function(){
		changeData('left');
	});
	//向右移动
	$('#remove').on('click',function(){
		changeData('right');
	});
	//监听行双击事件,
    table.on('rowDouble(rightTable)', function (obj) {
        changeData('left');
    });
	//重置
	$('#reset').on('click',function(){
        table.reload('leftTable', {
            url: '',
            data: config.oldLeftData
        });
        table.reload('rightTable', {
            url: '',
            data: config.oldRightData
        });
	});
	//保存
    $('#save').on('click', function () {
        var loadIndex = layer.load();//显示遮罩层
   		var url =config.addUrl;
		onSaveClick(url);
	});
	//监听行双击事件
    table.on('rowDouble(leftTable)', function (obj) {
    	var tableBox = $(this).parents('.layui-table-box');
    	var index = $(this).attr('data-index');
        var tableDiv = tableBox.find(".layui-table-body.layui-table-main");
    	var checkCell = tableDiv.find("tr[data-index=" + index + "]").find("td div.laytable-cell-checkbox div.layui-form-checkbox I");
	    if (checkCell.length>0) {
	        checkCell.click();
	    }
        changeData('right');
    });
    //监听行双击事件
    table.on('rowDouble(rightTable)', function (obj) {
        var tableBox = $(this).parents('.layui-table-box');
    	var index = $(this).attr('data-index');
        var tableDiv = tableBox.find(".layui-table-body.layui-table-main");
    	var checkCell = tableDiv.find("tr[data-index=" + index + "]").find("td div.laytable-cell-checkbox div.layui-form-checkbox I");
	    if (checkCell.length>0) {
	        checkCell.click();
	    }
        changeData('left');
    });
	//选中状态
    table.on('checkbox(rightTable)', function(obj){
        var rightcheckStatus = table.checkStatus('rightTable');
        config.rightData = rightcheckStatus.data;
    });
    //选中状态
    table.on('checkbox(leftTable)', function(obj){
        var leftcheckStatus = table.checkStatus('leftTable');
        config.leftData = leftcheckStatus.data;
    });
  
    //type向那边添加数据
    function changeData(type){
    	//左列表现有数据
    	var leftData = table.cache['leftTable'];
        var rightData =  table.cache['rightTable'];
        //临时存储左列表原始数据
        if(config.oldLeftData.length==0){
        	for(var i = 0;i<leftData.length;i++){
        		config.oldLeftData.push(leftData[i]);
        	}
        }
        //临时存储右列表原始数据
        if(config.oldRightData.length==0){
        	for(var i = 0;i<rightData.length;i++){
        		config.oldRightData.push(rightData[i]);
        	}
        }
    	if(type=='left'){//向左移动数据
		   var obj = addLeft(leftData,rightData);
    	}
    	if(type=='right'){//向右移动数据
		   var obj = addRight(leftData,rightData);
    	}
    	if(!obj)return;
    	table.reload('leftTable', {
            url: '',
            data: obj.leftData
        });
        table.reload('rightTable', {
            url: '',
            data: obj.rightData
        });
        rightTable.leftData=obj.leftData;
        config.leftData=[];
        config.rightData=[];
    }
   
   //向左列表添加数据
    function addLeft(leftData,rightData){
   	    //获取右列表选择数据
	    if(config.rightData.length==0){
            layer.msg("未选择项目！", { icon: 5, anim: 6 });
            return;
        }
        var r_d=[]; 
        //leftData.reverse()
        var d1 = [];
        for (var i = 0; i < rightData.length; i++) {
            if(rightData[i].LAY_CHECKED && rightData[i].LAY_CHECKED===true){
                delete rightData[i].LAY_CHECKED
                rightData[i].LAY_TABLE_INDEX && delete rightData[i].LAY_TABLE_INDEX
                 var obj = {
                     'LBEquipItemVO_LBItem_Id': rightData[i].LBSectionItemVO_LBItem_Id,
                     'LBEquipItemVO_LBItem_CName': rightData[i].LBSectionItemVO_LBItem_CName,
                     'LBEquipItemVO_LBItem_SName': rightData[i].LBSectionItemVO_LBItem_SName,
                     'LBEquipItemVO_LBItem_UseCode': rightData[i].LBSectionItemVO_LBItem_UseCode,
                     'LBEquipItemVO_LBEquipItem_Id': rightData[i].LBEquipItemVO_LBEquipItem_Id
                };
                leftData.push(obj)
                leftTable.config.positionId = rightData[i] ? rightData[i].LBSectionItemVO_LBItem_Id : null;//左列表刚加入时定位
                rightTable.config.positionId = rightData[i - 1] ? rightData[i - 1].LBSectionItemVO_LBItem_Id : null;//右列表刚加入时定位
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
	    if(config.leftData.length==0){
            layer.msg("未选择项目！", { icon: 5, anim: 6 });
            return;
        }
        var l_d=[]; 
        //rightData.reverse();
        var d1 = [];
        for (var i = 0; i < leftData.length; i++) {
            if(leftData[i].LAY_CHECKED && leftData[i].LAY_CHECKED===true){
                delete leftData[i].LAY_CHECKED
                leftData[i].LAY_TABLE_INDEX && delete leftData[i].LAY_TABLE_INDEX
                 var obj = {
                    'LBSectionItemVO_LBItem_Id': leftData[i].LBEquipItemVO_LBItem_Id,
                    'LBSectionItemVO_LBItem_CName': leftData[i].LBEquipItemVO_LBItem_CName,
                    'LBSectionItemVO_LBItem_SName': leftData[i].LBEquipItemVO_LBItem_SName,
                    'LBSectionItemVO_LBItem_UseCode': leftData[i].LBEquipItemVO_LBItem_UseCode,
                    'LBEquipItemVO_LBEquipItem_Id' : leftData[i].LBEquipItemVO_LBEquipItem_Id
                };
                rightData.push(obj)
                leftTable.config.positionId = leftData[i - 1] ? leftData[i - 1].LBEquipItemVO_LBItem_Id : null;//左列表刚加入时定位
                rightTable.config.positionId = leftData[i].LBEquipItemVO_LBItem_Id;//右列表刚加入时定位
            }else{
                delete leftData[i].LAY_TABLE_INDEX
                l_d.push(leftData[i])
            }
        }
        leftData = l_d;
        return {leftData:leftData,rightData:rightData};
    }

    function getEntity(){
    	var entity={},addList=[],delIDList=[];
    	var leftData = table.cache['leftTable'];
    	var oldData=config.oldLeftData;
    	//新增
    	for (var i = 0; i < leftData.length; i++) {
            var addFlag = true;//是新增的
            for (var j = 0; j < oldData.length; j++) {
                if (leftData[i].LBEquipItemVO_LBItem_Id == oldData[j].LBEquipItemVO_LBItem_Id) {
                   addFlag = false;
                    break;
                }
            }
            if (addFlag) {
                var DataTimeStamp = [0,0,0,0,0,0,0,0];
                addList.push({
                    LBEquip: { Id: document.getElementById("equipID").value, DataTimeStamp: DataTimeStamp },
                    LBItem: { Id: leftData[i].LBEquipItemVO_LBItem_Id, DataTimeStamp: DataTimeStamp }
                });
            }
        }
    	//删除 
    	for (var i = 0; i < oldData.length; i++) {
            var delFlag = true;//是删除的
            for (var j = 0; j < leftData.length; j++) {
                if (oldData[i].LBEquipItemVO_LBItem_Id == leftData[j].LBEquipItemVO_LBItem_Id) {
                    delFlag = false;//存在 不是删除的
                    break;
                }
            }
            if (delFlag) {
                delIDList.push(oldData[i].LBEquipItemVO_LBEquipItem_Id);
            }
        }
    	delIDList  = delIDList.join();
    	//第二个如果为true新增时 则判断实体是否已经存在 否则不判断
        var isCheckEntityExist = true;
        entity = { addEntityList: addList, isCheckEntityExist: isCheckEntityExist,delIDList:delIDList};
    	return entity;
    }
    //保存
	function onSaveClick(url) {
        //新增实体
        try {
            var entity = getEntity();
            var params = JSON.stringify(entity);

            var config = {
                type: "POST",
                url: url,
                data: params
            };
            uxutil.server.ajax(config, function (data) {
                layer.closeAll('loading');//隐藏遮罩层
                if (data.success) {
                    var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
                    parent.layer.close(index); //再执行关闭
                } else {
                    if (!data.msg) data.msg = '保存失败';
                    layer.msg(data.msg, { icon: 5, anim: 6 });
                }
            });
        } catch (err) {
            layer.closeAll('loading');//隐藏遮罩层
            layer.msg(err);
        }
	}
});