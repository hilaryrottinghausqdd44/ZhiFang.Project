/**
 * 检验评语
 * @author liangyl
 * @version 2021-05-21
 */layui.extend({
	 uxutil: 'ux/util',
	 uxbase: 'ux/base',
	uxtable: 'ux/table'
 }).use(['uxutil','uxbase','form','uxtable'], function(){
	var $ = layui.$,
		form = layui.form,
		uxtable = layui.uxtable,
		uxbase = layui.uxbase,
		uxutil = layui.uxutil;

   //短语查询服务
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBPhraseByHQL?isPlanish=true';
    /**修改服务地址*/
	var EDIT_URL = uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBPhraseByField';
	/**删除数据服务路径*/
	var DEL_URL = uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBPhrase';
    /**新增数据服务路径*/
	var ADD_URL = uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBPhrase';

    /**小组*/
	var SECTIONID = null;
	//检验评语内容
	var TESTINFO = '';
	/**小组检验评语名称*/
	var TYPENAME = '';
	//当前列表选择行
	var ROW_DATA =[];
	var DEFAULT_DATA = {},
	    AFTER_SAVE = function(){};//保存后回调函数
	    
    //列表配置
	var config = {
		elem: '#table',
		height:'full-160',
		title: '检验评语列表',
		initSort:false,
		defaultOrderBy:[{property:'LBPhrase_DispOrder',direction:'ASC'}],
		cols: [[
			{field:'LBPhrase_Id', width:180, title: 'ID', sort: false,hide:true},
			{field:'LBPhrase_CName', minWidth:150,flex:1, title: '短语名称',edit:'text',sort: false},
			{field:'LBPhrase_Shortcode', width:100, title: '快捷码', edit:'text',sort: false},
			{field:'LBPhrase_DispOrder', width:120, edit:'text',title: '显示次序'},
			{fixed: 'right', title:'操作', toolbar: '#barDemo', width:120}
		]],
		loading:true,
		page: false,
		size: 'sm',
		text: {none: '暂无相关数据' },
		parseData: function(res){ //res 即为原始返回的数据
			if(!res) return;
			var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
			return {
				"code": res.success ? 0 : 1, //解析接口状态
				"msg": res.ErrorInfo, //解析提示文本
				"count": data.count || 0, //解析数据长度
				"data": data.list || []
			};
		},
		done: function (res, curr, count) {
			if(count==0)CHECK_ROW=[];
		}
	};
	
	//列表实例
	var tableInd = uxtable.render(config);
	//监听行双击事件
	tableInd.table.on('row(table)', function(obj){
		//标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	    CHECK_ROW=obj;
	});
	 //监听行工具事件
    tableInd.table.on('tool(table)', function(obj){
	    var data = obj.data;
	    if(obj.event === 'del'){
	      layer.confirm('确定删除行吗？', function(index){
	      	if(obj.data.LBPhrase_Id){
	      		 uxutil.server.ajax({
					url: DEL_URL+'?id='+obj.data.LBPhrase_Id
				 }, function(data) {
					 layer.close(index);
					if(data.success === true) {
						uxbase.MSG.onSuccess("删除成功!");
	                  	 obj.del();
					}else{
						uxbase.MSG.onError("删除失败!");
					}
				});
	      	}else{
	      		obj.del();
	      		layer.close(index);
	      	}
	      });
	    } else if(obj.event === 'select'){
	    	var cname ="";
	    	if(obj.data.LBPhrase_CName)cname=obj.data.LBPhrase_CName;
	        $('#PhraseValue').val(cname);
	    }
	 });
	//新增
	$('#add').on('click',function(){
		var list = tableInd.table.cache.table;
    	list.push({
    		LBPhrase_Id: "",
    		LBPhrase_CName:"",
    		LBPhrase_Shortcode:"",
    		LBPhrase_DispOrder:list.length+1
    	});
    	tableInd.instance.reload({data:list});
    });
    //保存
    $('#save').on('click',function(){
		var list = tableInd.table.cache.table;
    	onSaveClick(list);
    });
     //保存
    $('#accept').on('click',function(){
    	if($("#isClose").prop("checked"))AFTER_SAVE($('#PhraseValue').val());
    });
    //刷新
    $('#refresh').on('click',function(){
		onSearch();
    });
    
    //查询按钮
    $('#search').on('click',function(){
		onSearch();
    });
    var saveErrorCount = 0,
		saveCount = 0,
		saveLength = 0;
    ///保存方法
	function onSaveClick(list){
        //显示遮罩
		if(list.length==0)return;
		var indexs=layer.load(2);
		saveErrorCount = 0;
		saveCount = 0;
		saveLength = list.length;
	
		for(var i=0;i<list.length;i++){
			var id = list[i].LBPhrase_Id;
			if(id){//修改
     			updateOne(list[i]);
			}else{//新增
				addOne(list[i]);
			}
		}
	}
	function getParams(obj){
		var entity = {
    		CName:obj.LBPhrase_CName,
    		Shortcode:obj.LBPhrase_Shortcode,
    		DispOrder:obj.LBPhrase_DispOrder
    	}
		return {entity:entity};
	}
	/**编辑一条*/
	function updateOne(obj){
        var params = getParams(obj);
        //编辑
		params.entity.Id= obj.LBPhrase_Id;
        params.fields ="Id,CName,Shortcode,DispOrder";
		//显示遮罩层
		var config = {
			type: "POST",
			url: EDIT_URL, 
			data: JSON.stringify(params)
		};
		uxutil.server.ajax(config, function(data) {
			if (data.success) {
				saveCount++;
			} else {
				saveErrorCount++;
			}				
			onSaveEnd();
		});
	}
	/**添加一条*/
	function addOne(obj){
        var params = getParams(obj);
        params.entity.IsUse = 1;
        params.entity.ObjectID =SECTIONID;
        params.entity.ObjectType = 1;
        params.entity.PhraseType = 'SamplePhrase';      
        params.entity.TypeName = TYPENAME;

		//显示遮罩层
		var config = {
			type: "POST",
			url: ADD_URL, 
			data: JSON.stringify(params)
		};
		uxutil.server.ajax(config, function(data) {
			if (data.success) {
				saveCount++;
			} else {
				saveErrorCount++;
			}				
			onSaveEnd();
		});
	}
	function onSaveEnd(){
		if (saveCount + saveErrorCount == saveLength) {
			layer.closeAll('loading');//隐藏遮罩层
			if (saveErrorCount == 0){
				uxbase.MSG.onSuccess("保存成功!");
			}else{
				uxbase.MSG.onError("保存失败!");
			}
		}
	}
	function getWhere(){
		var params = [],
		    where = "";
			//小组Id
		if(SECTIONID) {
			params.push("lbphrase.ObjectID=" + SECTIONID + "");
		}
		if(TYPENAME){
			params.push("lbphrase.TypeName='" + TYPENAME + "'");
		}
		params.push("lbphrase.ObjectType=1");
		
		if(params.length > 0) {
			where+= params.join(' and ');
		}
		
		if($('#searchText').val()){
			var hql="(lbphrase.CName like '%" + $('#searchText').val() + 
	    		"%' or lbphrase.Shortcode like '%" + $('#searchText').val() + "%')";
			where+= ' and '+ hql;
		}
		return where;
	}
    //数据加载
    function onSearch(){
		var cols = tableInd.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_LIST_URL+'&fields=' + fields.join(',')+'&sort='+JSON.stringify(tableInd.config.defaultOrderBy);
		if(getWhere())url +='&where='+getWhere();
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				
				tableInd.instance.reload({data:list});
			}else{
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	};
	//初始化数据
	window.initData = function(data,afterSave){
		if(typeof afterSave == 'function'){
			AFTER_SAVE = afterSave;
		}
		SECTIONID = data.SECTIONID;
		TESTINFO = data.TESTINFO;
		TYPENAME  = data.TYPENAME;
		//DEFAULT_DATA = data;
		//因为数据为页面外部调用传入，
		//layui的442行each处理中代码if(obj.constructor === Object)会将data与Object比对返回false
		//所以采用赋值方式创建新的Object，规避该判断
		DEFAULT_DATA = {};
		for(var key in data){
			//日期格式处理
			DEFAULT_DATA[key] = data[key];
		}
		
		$('#PhraseValue').val(TESTINFO);
		$('#PhraseValue').attr("placeholder","当前小组的"+TYPENAME);
		//初始化
	    onSearch();
	}

	
});