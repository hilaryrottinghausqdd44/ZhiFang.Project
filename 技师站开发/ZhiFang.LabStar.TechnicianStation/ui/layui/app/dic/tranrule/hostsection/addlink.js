/**
	@name：新增站点类型对应小组
	@author：liangyl
	@version 2021-08-17
 */
layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil','table'], function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		table = layui.table;
		
	//新增数据服务
	var ADD_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBTranRuleHostSection';
    //检验小组查询服务
	var GET_SECTION_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true";
      //获取指定实体字段的最大号
    var GET_MAXNO_URL =  uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetMaxNoByEntityField?entityName=LBTranRuleHostSection&entityField=DispOrder';

    //外部参数
	var PARAMS = uxutil.params.get(true);
    //站点类型ID
    var HOSTTYPEID = PARAMS.HOSTTYPEID;
    //已选数据，需剔除
    var DEFAULT_DATA = parent.ChildEmpData();
    //列表数据
    var DATA_LIST = [];
    //保存参数
    var saveErrorCount = 0,
		saveCount = 0,
		saveLength = 0;
		
	//检验小组选择列表实例
	var	table_ind0 = table.render({
		elem:'#link_table',
    	title:'检验小组列表',
    	height:'full-65',
    	size: 'sm', //小尺寸的表格
		initSort:false,
		limit: 5000,
		loading:true,
		page: false,
		cols: [[
			{type: 'checkbox',fixed: 'left'},
		    {type: 'numbers',title: '行号',fixed: 'left'},
			{field:'LBSection_Id',title:'ID',width:150,sort:true,hide:true},
			{field:'LBSection_CName',title:'小组名称',flex:1,sort:false},
			{field:'LBSection_EName',title:'英文名称',width:100,sort:false},
			{field:'LBSection_Shortcode',title:'快捷码',width:100,sort:false},
			{field:'LBSection_DispOrder',title:'次序',width:100,sort:false,hide:true},
			{field:'isAdd', width:60, title: 'isAdd', hide: true}
		]],
		defaultOrderBy: JSON.stringify([{property: 'LBSection_DispOrder',direction: 'ASC'}]),
		text: {none: '暂无相关数据' },
		
		parseData: function(res){ //res 即为原始返回的数据
			if(!res) return;
            var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
			if(data.list && data)data.list = changeResult(data.list);
			return {
				"code": res.success ? 0 : 1, //解析接口状态
				"msg": res.ErrorInfo, //解析提示文本
				"count": data.count || 0, //解析数据长度
				"data": data.list || []
			};
		},
		done: function (res, curr, count) {
			var tr = table_ind0.config.instance.layBody.find('tr:eq(0)');
			if(tr.length > 0){
				tr.click();
			}
		}
	});
	table_ind0.reload({data:[]});
	//模块类型列表
	table.on('row(link_table)', function(obj){
		//标注选中样式
	    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	});
	//按钮事件
	var active = {
		save: function() {//新增关系、
	         alert('add');
		    var list = getcheckdata();
            if(list.length ==0){ 
            	layer.msg('请先选择数据行');
            	return false;
            }
            onSave(list);//保存操作
		},
		search: function() {//查询
			onSearch();
		},
		close: function() {//关闭
			parent.layer.closeAll('iframe');
		}
	};
	$('.addlink .layui-btn').on('click', function() {
		var type = $(this).data('type');
		active[type] ? active[type].call(this) : '';
	});
	//列表查询
	function onSearch(){
		var where = [],url="";
		var searchValue = $("#search-input").val(),
			searchFields = $("#search-input").attr("fields").split('/');
		var where = [];
		//名称/简称
		if(searchValue){
			var searchWhere = [],
				len = searchFields.length;
				
			for(var i=0;i<len;i++){
				searchWhere.push("lbsection." + searchFields[i] + " like '%" + searchValue + "%'");
			}
			searchWhere = searchWhere.join(' or ');
			if(searchWhere.length > 0){
				searchWhere = '(' + searchWhere + ')';
			}
			where.push(searchWhere);
		}
			
		onLoad({"where":where.join(' and ')});
		$('#search-input').val(searchValue);
	}
	
	//加载数据
	function onLoad(whereObj,url){
		var cols = table_ind0.config.cols[0],
			fields = [];
			
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		table_ind0.reload({
			url:GET_SECTION_LIST_URL,
			where:$.extend({},whereObj,{
				fields:fields.join(',')
			})
		});
	}
	//获取选中的值
	function getcheckdata(){
		var checkStatus = table.checkStatus('link_table');
        var data = checkStatus.data;
        return data;
	}
	//单个新增
	function AddLink(entity){
		//显示遮罩层
		var config1 = {
			type: "POST",
			url: ADD_URL,
			data: JSON.stringify({entity: entity})
		};
		var index = layer.load();
		uxutil.server.ajax(config1, function(data) {
			if (data.success) {
			    saveCount++;
			} else {
				saveErrorCount++;
			}				
			if ( saveCount +  saveErrorCount ==  saveLength) {
				if ( saveErrorCount == 0){
					layer.msg('保存成功!',{icon:6,time:2000});
					parent.layer.closeAll('iframe');
					parent.afterUpdateAddLink(HOSTTYPEID);
				}else{
					layer.msg(data.ErrorInfo, { icon: 5});
				}
			}
		});
	}
	//保存
	function onSave(arr){
		saveErrorCount = 0;
		saveCount = 0;
		saveLength = arr.length;
    	for (var i = 0; i < arr.length; i++) {
            var entity = {
                SectionID: arr[i].LBSection_Id,
                HostTypeID: HOSTTYPEID
            }
            getMaxNo(function(val) {
	            entity.DispOrder = val;
	        });
            AddLink(entity);
        }
	}
	    	 //获取指定实体字段的最大号
    function getMaxNo(callback) {
        var me = this;
        var result = "";
        uxutil.server.ajax({
            url: GET_MAXNO_URL,
            async:false
        }, function (data) {
            if (data) {
                callback(data.ResultDataValue);
            } else {
               layer.msg(data.ErrorInfo, { icon: 5});
            }
        });
    }
	function changeResult(list){
		var arr = [],isExec=true;
		if(DEFAULT_DATA.length>0){ //剔除已选的人员
			for(var i=0;i<list.length;i++){
				isExec=true;
				for(var j=0;j<DEFAULT_DATA.length;j++){
					if(list[i].LBSection_Id == DEFAULT_DATA[j].LBTranRuleHostSection_SectionID ){
						isExec = false;
						break;
					}
				}
				if(isExec)arr.push(list[i]);
			}
		}
		if(DEFAULT_DATA.length==0)arr =list;
		return arr;
	}
	function init(){
		onSearch();
	}
	//初始化 
	init();
//	//初始化数据
//	window.initData = function(data,afterSave){
//		if(typeof afterSave == 'function'){
//			AFTER_SAVE = afterSave;
//		}
//				//默认数据
//		DEFAULT_DATA = data;
//		
//
//	};
});