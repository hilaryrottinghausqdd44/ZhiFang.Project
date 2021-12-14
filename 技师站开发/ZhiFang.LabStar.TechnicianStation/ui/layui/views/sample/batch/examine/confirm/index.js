/**
 * 批量检验确认（初审）-批量检验单审定
 * @author liangyl
 * @version 2021-05-13
 */layui.extend({
     uxutil: 'ux/util',
     uxbase: 'ux/base',
	uxbasic: 'views/sample/batch/uxbasic',
	formtable: 'views/sample/batch/examine/confirm/list',
	itemtable: 'views/sample/batch/examine/basic/itemlist',
	tableSelect: '../src/tableSelect/tableSelect'
 }).use(['element', 'uxutil','uxbase','form','uxbasic','laydate','formtable','itemtable','tableSelect'], function(){
	var $ = layui.$,
		form = layui.form,
		uxbasic = layui.uxbasic,
		uxbase = layui.uxbase,
		laydate = layui.laydate,
		formtable = layui.formtable,
		itemtable = layui.itemtable,
		tableSelect = layui.tableSelect,
		element = layui.element,
		uxutil = layui.uxutil;
		
	//小组ID
	var SECTIONID  = uxutil.params.get(true).SECTIONID;
	//获取就诊类型信息服务
    var GET_SICKTYPE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSickTypeByHQL?isPlanish=true';
    //获取科室服务
    var GET_DEPT_IDENTITY_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptIdentityByHQL?isPlanish=true';
    //检验人
    var GET_EMP_IDENTITY_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmpIdentityByHQL?isPlanish=true';
    //样本单检验确认服务
	var SAVE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormBatchConfirm';

    //查询条件临时变量
    var SEARCHOBJ ={};
	//样本单列表实例
	var table0_Ind = formtable.render({
		elem:'#table',
		height:'full-215',
		title:'样本单列表',
		size: 'sm'
	});

	table0_Ind.table.on('row(table)', function(obj){
		//标注选中样式
	    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        //当前检验单检验项目结果数据加载
	    table1_Ind.loadData({TestFormID:obj.data.LisTestForm_Id});
	});
	//当前检验单检验项目结果列表实例
	var table1_Ind = itemtable.render({
		elem:'#item_table',
		height:'full-215',
		title:'当前检验单检验项目结果',
		size: 'sm'
	});
	form.on('submit(search)', function(data){
		SEARCHOBJ = data.field;
		if(!data.field.GTestDate){
            uxbase.MSG.onWarn("检验日期不能为空!");
            return false;
		}
		var msg = uxbasic.isDateValid(data.field);
        if (msg != "") {
            uxbase.MSG.onWarn(msg);
            return false;
        }
        data.field.StartDate = data.field.GTestDate.split(" - ")[0];
        data.field.EndDate = data.field.GTestDate.split(" - ")[1];
        var obj = getSearchObj(data.field);
        //查询
        table0_Ind.loadData(obj);
	});

    //执行
    $('#save').on('click',function(){
    	if(!$("#ExecutorID").val()){
            uxbase.MSG.onWarn("检验人不能为空!");
			return;
   		}
   		if(table0_Ind.table.cache.table.length==0){
            uxbase.MSG.onWarn("检验单不能为空!");
			return;
   		}
   		layer.prompt({
			formType:2,
			value:'',
			title:'审批意见',
			yes:function(index,layero){
				var prompt = layero.find('.layui-layer-input');
				var value = prompt.val();
			    onSaveClick(value);
			}
		});
    });
    
    //关闭
    $('#close').on('click',function(){
    	parent.layer.closeAll('iframe');
    });
       //icon 前存在icon 则点击该icon 等同于点击input
    $("input.layui-input+.layui-icon").on('click', function () {
        if (!$(this).hasClass("myDate")) {
            $(this).prev('input.layui-input')[0].click();
            return false;//不加的话 不能弹出
        }
    });
     element.on('tab(tabs)', function(obj){
     	if(obj.index==1){
     		var win = $(window),
		    maxHeight = win.height()-200;

		    $('#success_info').height(maxHeight+'px');
		    $('#fail_info').height(maxHeight+'px');
     	}
     });
	
	//获取查询参数
	function getSearchObj(obj){
		var Flag1 = obj.Flag1 ? 1 : 0;//无阳性
		var Flag2 = obj.Flag2 ? 1 : 0;//无异常
		var Flag3 = obj.Flag3 ? 1 : 0;//无HH/LL
        var itemResultFlag = Flag1+','+Flag2+','+Flag3;
        
		return{
			ZFSysCheckStatus:obj.ZFSysCheckStatus ? true : false,//全部——智能审核成功样本
	        TestStatus:obj.TestStatus ? true : false,//全部——检验完成样本
	        SickTypeID:obj.SickTypeID,//就诊类型
	        StartDate:obj.StartDate,//检验日期开始日期
	        EndDate:obj.EndDate,//检验日期结束日期
	        DeptID:obj.DeptID,//送检科室
	        beginSampleNo:obj.sampleno_min,//开始样本号        
	        endSampleNo:obj.sampleno_max,//结束样本号
	        itemResultFlag:itemResultFlag,
	        SECTIONID:SECTIONID
		}
	}
	function getIds(){
		var testFormIDList = [];
		var  records = table0_Ind.table.checkStatus('table').data;
		for(var i =0;i<records.length;i++){
    		testFormIDList.push(records[i].LisTestForm_Id);
    	}
		return testFormIDList.join(',');
	}
	//执行
   function onSaveClick(memoInfo){
   	 
        var params={
        	testFormIDList:getIds(),
			empID:$("#ExecutorID").val(),
			empName:$("#ExecutorID option:checked").text(),
			memoInfo:memoInfo
        };	
        var config = {
			type:'post',
			url:SAVE_URL,
			data:JSON.stringify(params)
		};
       
		uxutil.server.ajax(config,function(data){
			if(data.success){
                uxbase.MSG.onSuccess("保存成功!");
				var obj = getSearchObj(SEARCHOBJ);
		        //查询
		        table0_Ind.loadData(obj);
                 //返回成功失败消息
                //成功消息
                $('#success_info').val('');
                //失败消息
                $('#success_info').val();
//				AFTER_SAVE(data.value);
			}else{
                uxbase.MSG.onError(data.ErrorInfo);
			}
		});
   }
    function init(){
		//日期初始化
        initDateListeners();
	     //初始化下拉框
		initSystemSelect();
	}
    //初始化
	init();
	 // 窗体大小改变时，调整高度显示
	$(window).resize(function() {
		var win = $(window),
		    maxHeight = win.height()-200;

	    $('#success_info').height(maxHeight+'px');
	    $('#fail_info').height(maxHeight+'px');
	});
    	
    //监听新日期控件
    function initDateListeners() {
        var me = this,
            today = new Date();
        //赋值日期框
        var defaultvalue = uxutil.date.toString(today, true) + " - " + uxutil.date.toString(today, true);
        uxbasic.initDate('GTestDate',defaultvalue,'form',true);
    }
    //初始化系统下拉框
	function initSystemSelect(){
		//就诊类型初始化
		SickTypeList('SickTypeName','SickTypeID');
		//科室初始化
		DeptList('DeptName','DeptID');
	    EmpList(function(list){
			var len = list.length,
				htmls = ['<option value="">请选择检验人</option>'];
				
			for(var i=0;i<len;i++){
				htmls.push('<option value="' + list[i].HREmpIdentity_HREmployee_Id + '">' + list[i].HREmpIdentity_HREmployee_CName + '</option>');
			}
			$("#ExecutorID").html(htmls.join(""));
			form.render('select');
			
		});
	}
	  //初始化就诊类型
    function SickTypeList(CNameElemID, IdElemID) {
        var CNameElemID = CNameElemID || null,
            IdElemID = IdElemID || null;
       	var fields = ['Id','CName'],
			url = GET_SICKTYPE_URL + '&where=lbsicktype.IsUse=1';
		url += '&fields=LBSickType_' + fields.join(',LBSickType_');
		var height =$('.layui-card').height()-180;
        if (!CNameElemID) return;
        tableSelect.render({
            elem: '#' + CNameElemID,	//定义输入框input对象 必填
            checkedKey: 'LBSickType_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'lbsicktype.CName,lbsicktype.Shortcode',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '名称/快捷码',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                height: height,
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'radio' },
                    { type: 'numbers', title: '行号' },
                    { field: 'LBSickType_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'LBSickType_CName', width: 200, title: '项目名称', sort: false },
                    { field: 'LBSickType_Shortcode', width: 120, title: '快捷码', sort: false }
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
                    $(elem).val(record["LBSickType_CName"]);
                    if (IdElemID) $("#" + IdElemID).val(record["LBSickType_Id"]);
                }else{
                	 $(elem).val("");
                    if (IdElemID) $("#" + IdElemID).val("");
                }
            }
        });
    }
      //初始化就诊类型
    function DeptList(CNameElemID, IdElemID) {
        var CNameElemID = CNameElemID || null,
            IdElemID = IdElemID || null;
       var fields = ['HRDept_Id','HRDept_CName'],
			url = GET_DEPT_IDENTITY_URL + "&where=(hrdeptidentity.IsUse=1 and hrdeptidentity.SystemCode='ZF_LAB_START' and hrdeptidentity.TSysCode='1001101')";
		url += '&fields=HRDeptIdentity_' + fields.join(',HRDeptIdentity_');
	    var height =$('.layui-card').height()-180;
        if (!CNameElemID) return;
        tableSelect.render({
            elem: '#' + CNameElemID,	//定义输入框input对象 必填
            checkedKey: 'HRDeptIdentity_HRDept_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'hrdeptidentity.HRDept.CName',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '名称',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                height: height,
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'radio' },
                    { type: 'numbers', title: '行号' },
                    { field: 'HRDeptIdentity_HRDept_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'HRDeptIdentity_HRDept_CName', width: 200, title: '科室名称', sort: false }
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
                    $(elem).val(record["HRDeptIdentity_HRDept_CName"]);
                    if (IdElemID) $("#" + IdElemID).val(record["HRDeptIdentity_HRDept_Id"]);
                }else{
                	 $(elem).val("");
                    if (IdElemID) $("#" + IdElemID).val("");
                }
            }
        });
    }
	
	//获取检验人
	function EmpList(callback){
        var fields = ['HREmployee_Id', 'HREmployee_CName'],
            labid = uxutil.cookie.get(uxutil.cookie.map.LABID),
            url = GET_EMP_IDENTITY_URL + "&where=(hrempidentity.IsUse=1 and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.TSysCode='1001001') and (LabID='" + labid + "')";;
		url += '&fields=HREmpIdentity_' + fields.join(',HREmpIdentity_');
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
                uxbase.MSG.onError(data.msg);
			}
		});
	}
});