layui.extend({
	uxutil: 'ux/util',
	uxtable: 'ux/table'
}).use(['uxutil', 'uxtable', 'form', 'laydate'], function () {
	var $ = layui.$,
		form = layui.form,
		laydate = layui.laydate,
		uxtable = layui.uxtable,
        uxutil = layui.uxutil;

	//获取新闻类型列表
	var GET_NEWS_TYPE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/UDTO_SearchBDictTreeByHQL?isPlanish=true&fields=BDictTree_Id,BDictTree_CName';
	//获取新闻消息列表服务地址
    var GET_NEWS_LIST_URL = uxutil.path.ROOT + "/ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileReadingUserListByHQLAndEmployeeID?isPlanish=true&isSearchChildNode=true&fields=FFile_Id,FFile_Title,FFile_No,FFile_Type,FFile_PublisherDateTime,FFile_IsHREmployeeReader";//
	//外部参数
    var PARAMS = uxutil.params.get(true);
	//新闻类型
    var NEW_TYPE_LIST = [];
	//员工信息
	var EMPID = uxutil.cookie.get(uxutil.cookie.map.USERID);
    var DEPTID = uxutil.cookie.get(uxutil.cookie.map.DEPTID);
	//表格
	var config = {
		elem:$("#table"),
		toolbar:'#table-toolbar-top',
		height:'full-40',
		defaultLoad:true,
		page:true,
		initSort: {
			field:'FFile_PublisherDateTime',//排序字段
			type:'desc'
		},
		cols:[[
			{field:'FFile_Id',title:'主键ID',width:180,hide:true},
            {
                field: 'FFile_IsHREmployeeReader', width: 120, title: '阅读状态', sort: true, templet: function (data) {
                    var result = '';
                    if (String(data.FFile_IsHREmployeeReader) == "true")
                        result = "<span style='color:green;'>已读</span>";
                    else if(String(data.FFile_IsHREmployeeReader) == "false")
						result = "<span style='color:red;'>未读</span>";
					return result;
                }
            },
			{field:'FFile_Title',minWidth:160,title:'标题',sort:false},
			{field:'FFile_No',width:140,title:'编号',sort:true},
            {
                field: 'FFile_Type', width: 140, title: '类型', sort: false, templet: function (data) {
                    var result = "";
                    $.each(NEW_TYPE_LIST, function (i, item) {
                        if (data.FFile_Type == item.BDictTree_Id) result = item.BDictTree_CName;
                        return false;
                    });
					return result;
                }
            },
            { field: 'FFile_PublisherDateTime', width: 160, title: '发布时间',sort:true },
			{ field:'FFile_IsUse',title: '操作',width: 80,align: 'center',toolbar: '#tableOperation' }
		]]
	};
    var tableInd = uxtable.render(config);
	//头工具栏事件
	tableInd.table.on('toolbar(table)', function(obj){
		switch(obj.event){
			case 'search':onSearch();break;
		}
	});
	//监听行双击事件
	tableInd.table.on('rowDouble(table)', function(obj){
		toDo(obj);
	});
	//监听行工具事件
    tableInd.table.on('tool(table)', function (obj) {
		var data = obj.data, //获得当前行数据
			layEvent = obj.event; //获得 lay-event 对应的值
		if (layEvent === 'detail') toDo(obj); //查看
    });
	//状态下拉监听
	form.on('select(newType)', function(data){
		onSearch();
	});
	//新闻时间范围下拉监听
	form.on('select(DateTimeRangeType)', function(data){
		var value = data.value;
		if(value != ""){
			var today = uxutil.date.toString(new Date(),true);
			var start = uxutil.date.toString(uxutil.date.getNextDate(today,1-parseInt(value)),true);

			LAYDATE_DATES.config.laydate.setValue(start + ' - ' + today);
			onSearch();
		}else{
			LAYDATE_DATES.config.laydate.setValue();
			onSearch();
		}
	});
	//日期时间范围
	var LAYDATE_DATES = laydate.render({
		elem:'#newDates',
		range:true,
		done: function(value,date,endDate){
			$('#DateTimeRangeType').val("");
			setTimeout(function(){
				onSearch();
			},100);
		}
	});
	//查询
	function onSearch(){
        var DateTimeRangeType = $('#DateTimeRangeType option:selected').val(),
            newDates = $("#newDates").val(),
            newTitle = $("#newTitle").val(),
			newType = $('#newType option:selected').val(),
			where = [];

		//条件=使用
		where.push("ffile.IsUse=1");

		if(newDates){
			var splitField = $("#newDates").attr("placeholder");
			var dateArr = newDates.split(splitField);
			where.push("ffile.PublisherDateTime >='" + dateArr[0] +"' and ffile.PublisherDateTime <'" +
				uxutil.date.toString(uxutil.date.getNextDate(dateArr[1]),true) + "'");
		}

		if(newTitle){
			where.push("ffile.Title like '%" + newTitle+"%'");
		}
		if(newType){
			where.push("ffile.Type=" + newType);
        }
		//执行查询
		onLoad({"where":where.join(' and ')});
		//赋值表单组件
        updateNewTypeSelect();
		$('#DateTimeRangeType').val(DateTimeRangeType);
		LAYDATE_DATES = laydate.render({
			elem:'#newDates',
			range:true,
			value:newDates,
			done: function(value,date,endDate){
				$('#DateTimeRangeType').val("");
				setTimeout(function(){
					onSearch();
				},100);
			}
		});
        $("#newDates").val(newDates);
        $("#newTitle").val(newTitle);
        $('#newType').val(newType);
        form.render('select');
	};
	//加载数据
	function onLoad(whereObj){
		var cols = config.cols[0],
			fields = [];

		for(var i in cols){
			fields.push('FFile_' + cols[i].field);
		}

		tableInd.reload({
			url:GET_NEWS_LIST_URL+'&sort=[{"property":"FFile_PublisherDateTime","direction":"DESC"}]',
			where:$.extend({},whereObj,{
				fields:fields.join(',')
			})
		});
    };
	//处理页面
	function toDo(obj){
        var data = obj.data;
		var url = uxutil.path.ROOT+"/ui/layui/views/news/detail/detail.html"//新闻详情页面
		if(url){
			layer.open({
				title:data.FFile_Title,
				type:2,
				content:url + '?id=' + data.FFile_Id + '&t=' + new Date().getTime(),
				maxmin:true,
				toolbar:true,
				resize:true,
                area: ['95%', '95%'],
                end: function () {
                    onSearch();
                }
            });
			//调用父页面获得新闻未读数量 -- 处理是否显示有未读标识
            parent.getUnreadNewsList();
		}
    };
	//获取新闻类型
	function loadNewTypeList(callback){
        var url = GET_NEWS_TYPE_LIST_URL+'&where=IsUse=true';

		uxutil.server.ajax({
			url:url
		},function(data){
            if (data.success) {
                NEW_TYPE_LIST = data.value.list || [];
                callback();
                updateNewTypeSelect();
			}else{
				layer.msg(data.msg);
			}
		});
    };
	//更新新闻类型下拉框
    function updateNewTypeSelect() {
		 var html = "<option value=''>选择新闻类型</option>";
        $.each(NEW_TYPE_LIST, function (i, item) {
            html += '<option value="' + item.BDictTree_Id + '">' + item.BDictTree_CName + '</option>';
        });
        $("#newType").html(html);
        form.render('select');
    };
	//初始化
    function init() {
        loadNewTypeList(function () {
            onSearch();
        });
    };
    init();
});