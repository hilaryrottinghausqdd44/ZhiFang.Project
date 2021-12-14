/**
	@name：检验小组权限
	@author：liangyl
	@version 2019-11-14
 */
layui.extend({
	uxutil:'/ux/util',
    uxtable:'/ux/table',
	emptable:'views/system/set/user/section/role/emplist',
	righttable:'views/system/set/user/section/role/rightlist',
	roletable:'views/system/set/user/section/role/rolelist'
}).use(['uxutil','table','emptable','righttable','roletable'],function(){
	var $ = layui.$,
		uxutil=layui.uxutil,
		emptable = layui.emptable,
		righttable = layui.righttable,
		roletable = layui.roletable,
		table = layui.table;
		
	//自定义变量
	var config={
		//人员选择行
		EmpRowData:[],
		//当前小组选择行
		RightRowData:[],
		//员工身份表
		tableInsEmp:null,
		//小组表
		tableInsRight:null,
		//角色表
		tableInsRole:null
	};  
	//初始化
    function init(){
        //人员表实例化
		var empObj={
		 	elem:'#employee_table',
	    	title:'人员列表',
	    	height:'full-95',
	    	where:"hrempidentity.TSysCode=1001001 and hrempidentity.SystemCode='ZF_LAB_START'",
	    	done: function(res, curr, count) {
	    		config.EmpRowData =[];
				if(count>0){
					//默认选择第一行
					var rowIndex = 0;
		            //默认选择行
				    doAutoSelect(this,rowIndex);
				}
			}
		};
		//人员表实例化
		config.tableInsEmp=emptable.render(empObj);
	
		 //检验中权限
		var rightObj={
		 	elem:'#right_table',
	    	title:'检验中权限列表',
	    	height:'full-95',
	    	done: function(res, curr, count) {
	    		config.RightRowData=[];
				if(count>0){
					//默认选择第一行
					var rowIndex = 0;
					//默认选择行
				    doAutoSelect(this,rowIndex);
				}else{
					if(config.tableInsRole){
						setTimeout(function() {
				        	config.tableInsRole.getLinkData(null,null);
				    	},200);
					}
				}
			}
		};
		//检验中权限实例对象
		config.tableInsRight=righttable.render(rightObj);
		
		 //角色对象
		var roleObj={
		 	elem:'#role_table',
	    	title:'角色列表',
	    	height:'full-95'
		};
		//取单时间段规则列表实例对象
		config.tableInsRole=roletable.render(roleObj);
	    //事件监听
	    initListeners();
    }
	/***默认选择行
	 * @description 默认选中并触发行单击处理 
	 * @param that:当前操作实例对象
	 * @param rowIndex: 指定选中的行
	 * */
	function doAutoSelect(that, rowIndex) {
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
	};
    //打开取单分类窗体
    function openWinForm(id){
		var win = $(window),
			maxWidth = win.width(),
			maxHeight = win.height(),
			width = maxWidth > 600 ? 600 : maxWidth,
			height = maxHeight > 422 ? 422 : maxHeight;
		var title = '新增取单时间分组'	;
		if(id)title = '编辑取单时间分组';
		layer.open({
			title:title,
			type:2,
			content:'form.html?id=' + id +'&t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:[width+'px',height+'px']
		});
	}
    function afterUpdate(data){
    	if(data && data.value )config.LBReportDateID = data.value.id;
    	if(data)config.tableIns.loadData({});
    }
    //根据取单分类联动
    function onSearch(obj){
	    setTimeout(function() {
    	    config.LBReportDateID=obj.LBReportDate_Id;
	    	//取单时间描述列表联动
	    	config.tableInsDesc.loadData({},config.LBReportDateID);
    	},200);
    }
  
    //事件监听
    function initListeners(){
		//选择员工列表选择行监听
		table.on('row(employee_table)', function(obj){
			config.EmpRowData =[] ;
			config.EmpRowData.push(obj.data);
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	        setTimeout(function() {
		    	config.tableInsRight.loadData({},obj.data.HREmpIdentity_HREmployee_Id);
	    	},200);
		});
	    //选择检验中权限(小组)列表选择行监听
		table.on('row(right_table)', function(obj){
			config.RightRowData =[];
			config.RightRowData.push(obj.data);
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	        setTimeout(function() {
	        	config.tableInsRole.getLinkData(obj.data.LBRight_EmpID,obj.data.LBRight_LBSection_Id);
	    	},200);
		});
		
		//选择小组
	    $('#saveSection').on('click',function(){
            config.tableInsRight.OpenWin(config.EmpRowData[0].HREmpIdentity_HREmployee_Id,config.EmpRowData[0].HREmpIdentity_HREmployee_CName);
		});
		
		//角色保存
		$('#saveRole').on('click',function(){
			if(config.EmpRowData.length == 0)return;
			config.tableInsRole.onSaveClick(config.EmpRowData[0].HREmpIdentity_HREmployee_Id,config.RightRowData[0].LBRight_LBSection_Id);
		});
		$('#search').on('click',function(){
			config.tableInsEmp.loadData();
		});
    }
    function afterSectionRoleUpdate(data){
        var me = this;
        layer.msg('保存成功!',{ icon: 6, anim: 0 ,time:2000 });
        config.tableInsRight.loadData({},config.EmpRowData[0].HREmpIdentity_HREmployee_Id);
    };
    window.afterSectionRoleUpdate = afterSectionRoleUpdate;
	//初始化
	init();
});