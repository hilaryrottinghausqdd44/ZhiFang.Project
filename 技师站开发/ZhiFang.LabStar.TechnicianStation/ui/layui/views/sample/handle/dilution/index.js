/**
 * 稀释样本结果处理
 * @author liangyl
 * @version 2021-05-15
 */
layui.extend({
	uxutil: 'ux/util',
	uxbase: 'ux/base',
	uxtable:'ux/table',
    tableSelect: '../src/tableSelect/tableSelect'
}).use(['table', 'uxutil','uxbase','form','tableSelect'], function(){
	var $ = layui.$,
		table = layui.table,
		form = layui.form,
		tableSelect = layui.tableSelect,
		uxbase = layui.uxbase,
		uxutil = layui.uxutil;
		
	//获取样本单数据服务路径
	var GET_TESTITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisTestItemByHQL?isPlanish=true';
	//获取项目服务路径
	var GET_ITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemByHQL?isPlanish=true';
	
	/**样本结果稀释处理*/
	var SAVE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_LisTestItemResultDilution';
	//排序
	var DEFAULTORDERBY = [{property:'LisTestItem_DispOrder',direction:'ASC'}];
  
	/**样本单ID*/
	var TESTFORMID= uxutil.params.get(true).TESTFORMID;
	//列表配置
	var config = {
		elem: '#table',
		height:'full-95',
		title: '样本单项目列表',
		initSort:false,
		cols: [[
		    {type: 'checkbox', fixed: 'left'},
			{field:'LisTestItem_Id', width:180, title: 'ID', sort: true,hide:true},
			{field:'LisTestItem_LBItem_Id', width:100, title: '检验项目编号', sort: true,hide:true},
			{field:'LisTestItem_LBItem_CName', width:180, title: '检验项目名称', sort: true},
			{field:'LisTestItem_LBItem_SName', width:120, title: '检验项目简称'},
			{field:'LisTestItem_ReportValue', width:100, title: '报告值'},
			{field:'LisTestItem_OriglValue',width:100,title:'原始值',sort:true},
			{field: 'LisTestItem_ReportStatusID', width: 80, title: '状态', sort: true },
			{field: 'LisTestItem_RefRange', width: 100, title: '参考值', sort: false }
		]],
		loading:true,
		page: false,
		limit: 500,
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
		done: function(res, page, count){
			select(res.data,true);
		}
	};
	//列表实例
	var tableInd = table.render(config);
	//确认查询功能
	$('#search').on('click',function(){
		onSearch();
	});
	//全选
	$('#allselect').on('click',function(){
    	var data = table.cache['table'];
        if(data.length>0)select(data,true);
         form.render('checkbox');//这个地方必须加上重新渲染 否则没有状态变化
	});
	//全不选
	$('#unallselect').on('click',function(){
		var data = table.cache['table'];
        if(data.length>0)select(data,false);
         form.render('checkbox');//这个地方必须加上重新渲染 否则没有状态变化
	});
	
    //执行
    $('#save').on('click',function(){
    	var  records = table.checkStatus('table').data;
		if (records.length == 0) {
			uxbase.MSG.onWarn("请选择行");
			return;
		}
		var Multiple = $('#InputMultiple').val();
		//校验 校验稀释倍数。稀释倍数不能小于等于0；不能等于1，等于1没有意义。当稀释系数＜1，提示稀释倍数＜1，确定要执行稀释样本结果调整吗？
		if(Multiple==0 || Multiple==1 ){
			uxbase.MSG.onWarn("稀释倍数不能小于等于0；不能等于1");
			return;
		}
		if(Multiple<1){
			layer.confirm('稀释倍数＜1,确定要执行稀释样本结果调整吗?',{ icon: 3, title: '提示' }, function(index) {
			    onSaveUpdate(records,Multiple);
		    });
		}else{
			onSaveUpdate(records,Multiple);
		}
    });
    
    //关闭
    $('#close').on('click',function(){
    	parent.layer.closeAll('iframe');
    });
     //icon 前存在icon 则点击该icon 等同于点击input
    $("input.layui-input+.layui-icon").on('click', function () {
        $(this).prev('input.layui-input')[0].click();
        return false;//不加的话 不能弹出
    });
	 
	//查询
	function onSearch(){
		var where = [];
		var	ItemID = $("#LBItem_ID").val();
	
		//样本单IDId
		if(TESTFORMID) {
			where.push("listestitem.LisTestForm.Id=" + TESTFORMID + "");
		}
		if(ItemID){
			where.push("listestitem.LBItem.Id=" + ItemID + "");
		}
		onLoad({"where":where.join(' and ')});
		
	}
	//加载数据
	function onLoad(whereObj){
		var cols = config.cols[0],
			fields = [];
			
		for(var i in cols){
			fields.push(cols[i].field);
		}
		tableInd.reload({
			url:GET_TESTITEM_LIST_URL,
			where:$.extend({},whereObj,{
				fields:fields.join(','),
				sort:JSON.stringify(DEFAULTORDERBY)
			})
		});
	}

    /**判断报告值是否是定量结果
	 * true:数值型的，false：非数值型
	 * */
    function isRealNum(val){
	    // isNaN()函数 把空串 空格 以及NUll 按照0来处理 所以先去除
	    if(val === "" || val ==null){
	        return false;
	    }
	    if(!isNaN(val)){
	        return true;
	    }else{
	        return false;
	    }
	}  
	 //结果单位下拉框监听 同步输入框
    form.on('select(Multiple)', function (data) {
        $("#InputMultiple").val(data.value);
    });
	//初始化
	function init(){
		initSystemSelect();
		onSearch();
	}
	
    //初始化系统下拉框
	function initSystemSelect(){
		//初始化项目下拉框
        initItem("LBItem_CName", "LBItem_ID");
	}
	  //初始化项目下拉框
    function initItem(CNameElemID, IdElemID) {
        var CNameElemID = CNameElemID || null,
            IdElemID = IdElemID || null;
        var fields = ['Id','CName','SName','Shortcode'],
			url = GET_ITEM_LIST_URL + "&where=(lbitem.IsUse=1)";
	    	url += '&fields=LBItem_' + fields.join(',LBItem_');
           
        if (!CNameElemID) return;
        tableSelect.render({
            elem: '#' + CNameElemID,	//定义输入框input对象 必填
            checkedKey: 'LBItem_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'lbitem.CName,lbitem.Shortcode,lbitem.SName',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '小组名称/简称/代码',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                height: '200',
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'radio' },
                    { type: 'numbers', title: '行号' },
                    { field: 'LBItem_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'LBItem_CName', width: 200, title: '项目名称', sort: false },
                    { field: 'LBItem_SName', width: 150, title: '简称', sort: false },
                    { field: 'LBItem_Shortcode', width: 120, title: '快捷码', sort: false },
                    { field: 'LBItem_DispOrder', width: 80, title: '排序', sort: false, hide: true }
                ]],
                text: { none: '暂无相关数据' },
                response: function () {
                    return {
                        statusCode: true, //成功状态码
                        statusName: 'code', //code key
                        msgName: 'msg ', //msg key
                        dataName: 'data' //data key
                    }
                },
                parseData: function (res) {//res即为原始返回的数据
                    if (!res) return;
                    var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                    return {
                        "code": res.success ? 0 : 1, //解析接口状态
                        "msg": res.ErrorInfo, //解析提示文本
                        "count": data.count || 0, //解析数据长度
                        "data": data.list || []
                    };
                }
            },
            done: function (elem, data) {
                //选择完后的回调，包含2个返回值 elem:返回之前input对象；data:表格返回的选中的数据 []
                if (data.data.length > 0) {
                    var record = data.data[0];
                    $(elem).val(record["LBItem_CName"]);
                    if (IdElemID) $("#" + IdElemID).val(record["LBItem_Id"]);
                    onSearch();
                }else{
                	 $(elem).val("");
                    if (IdElemID) $("#" + IdElemID).val("");
                }
            }
        });
    }
	/**
	 * 全选，全不选
	 * */
	function select(data,bo){
	   // 全选按钮选中
	   $('.layui-table-header input[name="layTableCheckbox"]').prop('checked', bo)
	   $('.layui-table-header input[name="layTableCheckbox"]').next().addClass('layui-form-checked')
	   // 其他按钮选中
	   data.forEach((element, index) => {
	     element["LAY_CHECKED"]='true'
	     $('tr[data-index=' + index + '] input[type="checkbox"]').prop('checked', bo);
	     $('tr[data-index=' + index + '] input[type="checkbox"]').next().addClass('layui-form-checked');
	   });
	}
	function onSaveUpdate(res,Multiple){
    	var LisTestItemID = "";
   	    for(var i=0;i<res.length;i++){
   	    	var ReportValue = res[i].LisTestItem_ReportValue;
   	    	var isExec = isRealNum(ReportValue);
   	    	if(!isExec){
				uxbase.MSG.onWarn("选中行中存在不是定量结果的报告值,不能进行样本结果稀释处理!");
	  		    LisTestItemID="";
	    		return;
	    	}
   	    	if(i>0)LisTestItemID+=','
   	    	LisTestItemID += res[i].LisTestItem_Id;
   	    }
	    var params ={
	    	testItemIDList:LisTestItemID,
	    	dilutionTimes: Multiple
	    };
	    var config = {
			type:'post',
			url:SAVE_URL,
			data:JSON.stringify(params)
		};
	    var index = layer.load();
	    uxutil.server.ajax(config,function(data){
	    	layer.close(index);
			if(data.success){
				uxbase.MSG.onSuccess("保存成功!");
				onSearch();
			}else{
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
    }
	//初始化
	init();
});