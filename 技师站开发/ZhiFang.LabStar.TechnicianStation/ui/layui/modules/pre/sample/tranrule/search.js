/**
 * @name：modules/pre/sample/tranrule/searchbar 样本分发查询工具栏
 * @author：liangyl
 * @version 2021-10-13
 */
layui.extend({
    CommonSelectUser: 'modules/common/select/preuser',
    CommonSelectDept: 'modules/common/select/dept',
    CommonSelectSampleType: 'modules/common/select/sampletype',
    CommonSelectSection: 'modules/common/select/section',
    tableSelect: '../src/tableSelect/tableSelect'
}).define(['uxutil','laydate', 'form', 'CommonSelectUser', 'CommonSelectDept','CommonSelectSickType','CommonSelectSampleType','CommonSelectSection','tableSelect'],function(exports){
	"use strict";
	
	var $ = layui.$,
		laydate = layui.laydate,
		form = layui.form,
        uxutil = layui.uxutil,
        CommonSelectUser = layui.CommonSelectUser,
        CommonSelectDept = layui.CommonSelectDept,
        CommonSelectSection = layui.CommonSelectSection,
        CommonSelectSickType = layui.CommonSelectSickType,
        CommonSelectSampleType = layui.CommonSelectSampleType,
        tableSelect = layui.tableSelect,
		MOD_NAME = 'SearchBar';

    //获取检验项目
    var GET_TESTITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemListByHQL?isPlanish=true&sort=[{property:"LBItem_DispOrder",direction:"ASC"}]';
    //根据小组获取小组项目
    var GET_SECTIONITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleSignForGetAllItemIdBySectionID';

    //查询工具栏dom
	var SEARCH_DOM = [
		'<div class="layui-form" id="{domId}-form" lay-filter="{domId}-form" style="margin-bottom:0; padding-bottom:0;">',
		  '<div class="layui-form-item">',
		     '<div class="layui-inline">', 
		        '<label class="layui-form-label">标本状态:</label>', 
		        '<div class="layui-input-inline" >', 
		           '<select name="{domId}-barCodeStatus" id="{domId}-barCodeStatus"  lay-search="" lay-filter="{domId}-barCodeStatus"><option value="">请选择</option></select>',
		     '</div>',
		     '<div class="layui-inline">', 
		        '<label class="layui-form-label">时间类型:</label>', 
		        '<div class="layui-input-inline" style="width: 120px;">', 
		         '<select name="{domId}-dateType" id="{domId}-dateType" lay-search="" lay-filter="{domId}-dateType"></select>', 
		        '</div>',
		        '<div class="layui-input-inline" style="width:190px;">', 
		         '<input type="text" id="{domId}-gDate" name="{domId}-gDate" class="layui-input myDate" placeholder="开始时间-结束时间" />', 
		         '<i class="layui-icon layui-icon-date"></i>',
		        '</div>', 
		       '</div>', 
		     '<div class="layui-inline">', 
		        '<label class="layui-form-label">开单科室:</label>', 
		        '<div class="layui-input-inline" >', 
	                '<select name="{domId}-DeptId" id="{domId}-DeptId" lay-search="" lay-filter="{domId}-DeptId"> <option value="">{text}</option></select>',
		        '</div>', 
		     '</div>',
		     '<div class="layui-inline">', 
		        '<label class="layui-form-label">检验小组:</label>', 
		        '<div class="layui-input-inline" >', 
	                '<select name="{domId}-SectionId" id="{domId}-SectionId" lay-search="" lay-filter="{domId}-SectionId"> <option value="">请选择</option></select>',
		        '</div>', 
		     '</div>',
		  '</div>',
		  '<div class="layui-form-item">',
		     '<div class="layui-inline">', 
		        '<label class="layui-form-label">条码号:</label>', 
		        '<div class="layui-input-inline">', 
		          	'<input type="text" name="{domId}-BarCode" id="{domId}-BarCode" autocomplete="off" class="layui-input" />',
		        '</div>', 
		     '</div>',
		     '<div class="layui-inline">', 
		        '<label class="layui-form-label">病历号:</label>', 
		        '<div class="layui-input-inline" >', 
		          	'<input type="text" name="{domId}-PatNo" id="{domId}-PatNo" autocomplete="off" class="layui-input" />',
		        '</div>', 
		     '</div>',
		      '<div class="layui-inline">', 
		        '<label class="layui-form-label">姓名:</label>', 
		        '<div class="layui-input-inline" >', 
		          	'<input type="text" name="{domId}-CName" id="{domId}-CName" autocomplete="off" class="layui-input" />',
		        '</div>', 
		     '</div>',
		        '<div class="layui-inline">', 
		        '<label class="layui-form-label">样本号:</label>', 
		        '<div class="layui-input-inline" >', 
		          	'<input type="text" name="{domId}-SapmleNo" id="{domId}-SapmleNo" autocomplete="off" class="layui-input" />',
		        '</div>', 
		     '</div>',
		  '</div>',
		  '<div class="layui-form-item">',
		    '<div class="layui-inline">', 
		        '<label class="layui-form-label">就诊类型:</label>', 
		        '<div class="layui-input-inline" >', 
	                '<select name="{domId}-SickTypeID" id="{domId}-SickTypeID" lay-search="" lay-filter="{domId}-SickTypeID"> <option value="">请选择</option></select>',
		        '</div>', 
		     '</div>',
		     '<div class="layui-inline">', 
		        '<label class="layui-form-label">样本类型:</label>', 
		        '<div class="layui-input-inline" >', 
	                '<select name="{domId}-SampleTypeID" id="{domId}-SampleTypeID" lay-search="" lay-filter="{domId}-SampleTypeID"> <option value="">请选择</option></select>',
		        '</div>', 
		     '</div>',
		     '<div class="layui-inline">', 
		        '<label class="layui-form-label">检验项目:</label>', 
		        '<div class="layui-input-inline" >', 
		        	'<input type="text" name="{domId}-ItemID" id="{domId}-ItemID"  class="layui-input layui-hide" />',
	                '<input type="text" name="{domId}-ItemName"  id="{domId}-ItemName" placeholder="请选择" readonly="" autocomplete="off" class="layui-input" />',
                    '<i class="layui-icon layui-icon-triangle-d"></i>',
		        '</div>',
		     '</div>',
		      '<div class="layui-inline">', 
		        '<label class="layui-form-label">住院号:</label>', 
		        '<div class="layui-input-inline">', 
		          	'<input type="text" name="{domId}-InPatNo" id="{domId}-InPatNo" autocomplete="off" class="layui-input" />',
		        '</div>', 
		     '</div>',
		     '<div class="layui-inline" style="padding-left: 5px;">', 
		        '<button type="button" id="{domId}-btn" class="layui-btn layui-btn-xs"><i class="layui-icon layui-icon-search"></i>查询</button>', 
		       '</div>', 
		    '</div>',
		     
		  '</div>',
		'</div>',
		'<style>',
            '.layui-form-item .layui-inline{margin-bottom: 2px; margin-right: 0px;}',
	        '.layui-form-item{margin-bottom: 0px;}',
	        '.layui-input + .layui-icon { cursor:pointer;position: absolute;top: 0px;right: 6px;color: #009688; }',
		'</style>'
	];
	//小组对应项目
    var SectionItemsMap = {};
	//查询工具栏
	var SearchBar = {
		searchId:null,//列表ID
		tableToolbarId:null,//列表功能栏ID
		//对外参数
		config:{
			domId:null,
			height:null,
			//过滤的开单科室条件
			defalutDeptID:null,
			//过滤的检验小组条件
			defalutSectionID:null,
	        //过滤的样本类型条件
			defalutSampleTypeID:null,
			//默认查询已签收样本日期范围,选已签收时 显示默认的时间范围
			defalutDate:null,
			//查询条件时间类型  ---时间类型下拉内容
			defalutDateType:null,
			//查询按钮触发事件
			searchClickFun:function(){}
		},
		//内部列表参数
		searchConfig:{
			elem:null
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,SearchBar.config,setings);
		me.searchConfig = $.extend({},me.searchConfig,SearchBar.searchConfig);
		if(me.config.height)me.searchConfig.height = me.config.height;
		me.searchId = me.config.domId;
		me.searchConfig.elem = "#" + me.searchId;
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		$('#' + me.config.domId).html('');
		var html = SEARCH_DOM.join("").replace(/{domId}/g,me.searchId).replace(/{tableToolbarId}/g,me.tableToolbarId);
		$('#' + me.config.domId).append(html);
		 //时间范围初始化
	    me.initDateHtml();
		 //样本类型
        CommonSelectSampleType.render({
            domId: me.config.domId + '-SampleTypeID', done: function () {
                if(me.config.defalutSampleTypeID)$("#" + me.config.domId + "-SampleTypeID").val(me.config.defalutSampleTypeID);
            }
        });
        //开单科室
        CommonSelectDept.render({
            domId: me.config.domId + "-DeptId", code: '1001101', done: function () {
                if(me.config.defalutDeptID)$("#" + me.config.domId + "-DeptId").val(me.config.defalutDeptID);
            }
        });
        //检验小组
        CommonSelectSection.render({
            domId: me.config.domId + "-SectionId",  done: function () {
                if(me.config.defalutSectionID)$("#" + me.config.domId + "-SectionId").val(me.config.defalutSectionID);
                form.render('select');
            }
        });
         //就诊类型
        CommonSelectSickType.render({ 
        	domId: me.config.domId + '-SickTypeID'
        });
        //时间类型   lisbarcodeform.LisOrderForm.OrderTime-开单时间,lisbarcodeform.InceptTime-签收时间,lisbarcodeform.CollectTime-采样时间,lisbarcodeform.ArriveTime-送达时间
        if(me.config.defalutDateType){
        	var date1 = me.config.defalutDateType.split(',');
        	var htmls = [];
        	for(var i in date1){
        		var arr = date1[i].split('-');
        		htmls.push("<option value='" + arr[0] +"'>" + arr[1] + "</option>");
        	}
			$('#'+me.config.domId + '-dateType').html(htmls.join(""));
			form.render('select');
        }
        me.ItemList(me.config.domId+'-ItemName',me.config.domId+'-ItemID');
	   
         //标本状态
        uxutil.server.ajax({
            url: uxutil.path.ROOT + "/ServerWCF/CommonService.svc/GetClassDic",
            data: { classname: "Pre_OrderSignFor_SapmpleStatus" }//, classnamespace:"PreParaEnumTypeEntity"
        }, function (data) {
            var htmls = ["<option value=''>请选择</option>"];
            if (data.success) {
                var list = data.value || [];
                for (var i = 0; i < list.length; i++) {
                    htmls.push('<option value="' + list[i].Memo + '">' + list[i].Name + '</option>');
                }
            } else {
                layer.msg(data.msg);
            }
            $("#" + me.config.domId + '-barCodeStatus').html(htmls.join(""));
        });

	};
	 //时间范围初始化
    Class.prototype.initDateHtml = function(){
    	var me = this;
    	var today = new Date();
		var defaultvalue = uxutil.date.toString(today, true) + " - " + uxutil.date.toString(today, true);
    	$('#' + me.config.domId+"-gDate").val(defaultvalue);
      //监听日期图标
		 $('#' + me.config.domId+"-gDate+i.layui-icon").on("click", function () {
			 var elemID = $(this).prev().attr("id");
			 if ($("#" + elemID).hasClass("layui-disabled")) return false;
			 var key = $("#" + elemID).attr("lay-key");
			 if ($('#layui-laydate' + key).length > 0) {
				 $("#" + elemID).attr("data-type", "date");
			 } else {
				 $("#" + elemID).attr("data-type", "text");
			 }
			 var datatype = $("#" + elemID).attr("data-type");
			 if (datatype == "text") {
				//时间范围
				laydate.render({
					 elem: '#' + me.config.domId+"-gDate",
					 eventElem:me.config.domId+'-gDate+i.layui-icon',
					 type: 'date',
					 range: true,
					 show:true
				 });
				 $("#" + elemID).attr("data-type", "date");
			 } else {
				 $("#" + elemID).attr("data-type", "text");
				 var key = $("#" + elemID).attr("lay-key");
				 $('#layui-laydate' + key).remove();
			 }
		 });
		 //监听日期input -- 不弹出日期框
		 $('#' + me.config.domId+"-form").on('focus', '#' + me.config.domId+'gDate', function () {
			 me.preventDefault();
			 layui.stope(window.event);
			 return false;
		 });
	};
    //获取按钮查询条件
	Class.prototype.getWhere = function(){
		var me = this,
		    where ="",//已送检查询条件
			values = form.val(me.config.domId + '-form'),
			startDate='',endDate='',
	        CName = values[me.config.domId + '-CName'], //病人姓名
	        DeptId = values[me.config.domId + '-DeptId'], //开单科室
	        BarCode = values[me.config.domId + '-BarCode'], //条码号
	        InPatNo = values[me.config.domId + '-InPatNo'], //住院号
	        ItemID = values[me.config.domId + '-ItemID'], //项目
	        SampleTypeID = values[me.config.domId + '-SampleTypeID'], //样本类型
	        SapmleNo = values[me.config.domId + '-SapmleNo'], //样本号
	        SectionID = values[me.config.domId + '-SectionId'], //检验小组
	       	SickTypeID = values[me.config.domId + '-SickTypeID'], //就诊类型
	        barCodeStatus = values[me.config.domId + '-barCodeStatus'], //样本状态
	        PatNo = values[me.config.domId + '-PatNo'], //病历号
	        dateType = values[me.config.domId + '-dateType'], //时间类型
	        gDate = values[me.config.domId + '-gDate']; //时间范围 
        if(gDate){
        	startDate = gDate.substring(0,10); //开始日期
            endDate = gDate.substring(13,gDate.length); //结束时间
        }      
		var arr=[],relationForm=[];
		//病人姓名
		if(CName)arr.push("lisbarcodeform.LisPatient.CName='"+CName+"'");
		//条码号
		if(BarCode)arr.push("lisbarcodeform.BarCode='"+BarCode+"'");
		//开单科室
		if(DeptId)arr.push("lisbarcodeform.LisPatient.DeptID="+DeptId);
		//样本类型
		if(SampleTypeID)arr.push("lisbarcodeform.SampleTypeID="+SampleTypeID);
		//住院号
		if(InPatNo)arr.push("lisbarcodeform.LisPatient.InPatNo='"+InPatNo+"'");
        //病历号
		if(PatNo)arr.push("lisbarcodeform.LisPatient.PatNo='"+PatNo+"'");
        //就诊类型
        if(SickTypeID)arr.push("lisbarcodeform.LisPatient.SickTypeID="+SickTypeID);
          //时间类型
		if(dateType && startDate && endDate){
			arr.push(dateType + " between '" + startDate + ' 00:00:00'+"' and '" + endDate + " 23:59:59'");
		}
        //样本状态
        if(barCodeStatus)arr.push(barCodeStatus);
        //样本号
		if(SapmleNo){
            arr.push("lisbarcodeform.Id=listestform.SampleForm.Id and listestform.GSampleNo='"+SapmleNo+"'");
			if (relationForm.indexOf("LisTestForm listestform") == -1) relationForm.push("LisTestForm listestform");
		}
        //小组
		if (SectionID && SectionItemsMap[SectionID]) {
            arr.push("lisbarcodeform.Id=lisbarcodeitem.LisBarCodeForm.Id and lisbarcodeitem.BarCodesItemID in (" + SectionItemsMap[SectionID] + ")");
            if (relationForm.indexOf("LisBarCodeItem lisbarcodeitem") == -1) relationForm.push("LisBarCodeItem lisbarcodeitem");
        }
		//项目
        if (ItemID) {
            arr.push("lisbarcodeform.Id=lisbarcodeitem.LisBarCodeForm.Id and lisbarcodeitem.BarCodesItemID=" + ItemID);
            if (relationForm.indexOf("LisBarCodeItem lisbarcodeitem") == -1) relationForm.push("LisBarCodeItem lisbarcodeitem");
        }
        
	    //查询条件
		if(arr.length>0)where = arr.join(' and ');
	    //入参说明 relationForm 运送人不在barcodeform表里 而在操作记录表
		return {
			where : where, //查询条件
			relationForm:relationForm.join()
		};
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		var today = uxutil.date.toString(new Date(), true);
		//默认设置的签收时间
		var InceptTime = (me.config.defalutDate && Number(me.config.defalutDate) > 0) ? uxutil.date.toString(uxutil.date.getNextDate(today, -me.config.defalutDate), true)+'-'+today : ""; 
		if($('#'+me.config.domId + '-dateType').val() == 'lisbarcodeform.InceptTime'){
			if(InceptTime)$('#'+me.config.domId + '-gDate').val(InceptTime);
		}
		form.on('select('+me.config.domId + '-dateType)', function(data){
		    var value = '';
		    if(data.value == 'lisbarcodeform.InceptTime'){
			   value= InceptTime;
		    }else if(data.value == 'lisbarcodeform.DataAddTime'){
		       value = today + " - " + today;
		    }
		    $('#'+me.config.domId + '-gDate').val(value);
		});      
		//下拉框 -- icon 前存在icon 则点击该icon 等同于点击input
	    $("input.layui-input+.layui-icon").on('click', function () {
	        if (!$(this).hasClass("myDate")) {
	            $(this).prev('input.layui-input')[0].click();
	            return false;//不加的话 不能弹出
	        }
	    });
		form.on('select('+me.config.domId+'-DeptId)', function(data){
			if(me.config.IsDefaultDept=='1'){ //还原记录默认科室
		        me.insertHistoryInfo({DeptID:data.value});
		    }
		});      
		//查询
		$('#'+me.config.domId+'-btn').on('click',function(){
			me.config.searchClickFun && me.config.searchClickFun(me.getWhere());
		});
		//检验小组
	    form.on('select(' + me.config.domId + '-SectionId)', function (data) {
            var value = data.value; //得到被选中的值
            if(!value)return false;
            if (SectionItemsMap[value]) return false;
            me.initSectionItem(value, function (itemids) {
                SectionItemsMap[value] = itemids;
            });
        });
	};
	//初始化检验小组下拉选择项
    Class.prototype.ItemList =  function(CNameElemID, IdElemID) {
    	var me = this,
            CNameElemID = CNameElemID || null,
            IdElemID = IdElemID || null;
        var fields = ['Id','CName','SName','Shortcode'],
			url = GET_TESTITEM_LIST_URL + "&where=lbitem.IsUse=1";
		url += '&fields=LBItem_' + fields.join(',LBItem_');
        if (!CNameElemID) return;
        var win = $(window),
		    maxheight = win.height();
        var height = maxheight - $("#"+me.config.domId+'-form').height() -230;
        tableSelect.render({
            elem: '#' + CNameElemID,	//定义输入框input对象 必填
            checkedKey: 'LBItem_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'lbitem.CName,lbitem.Shortcode,lbitem.SName',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '小组名称/简称/代码',	//搜索输入框的提示文字 默认关键词搜索
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
                    { field: 'LBItem_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'LBItem_CName', width: 200, title: '小组名称', sort: false },
                    { field: 'LBItem_SName', width: 150, title: '简称', sort: false },
                    { field: 'LBItem_Shortcode', width: 120, title: '快捷码', sort: false }
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

                }else{
                	 $(elem).val("");
                    if (IdElemID) $("#" + IdElemID).val("");
                }
            }
        });
    };
    //根据小组查询该小组下项目
    Class.prototype.initSectionItem = function (sectionid,callback) {
        var me = this,
            url = GET_SECTIONITEM_LIST_URL;
        if (!sectionid) callback && callback('');
        uxutil.server.ajax({
            url: url,
            type:'POST',
            data: JSON.stringify({ sectionID: sectionid })
        }, function (data) {
            var htmls = '';
            if (data.success) {
                if (data.ResultDataValue != "") htmls = data.ResultDataValue;
            } else {
                layer.msg(data.msg);
            }
            callback && callback(htmls);
        });
    };
   
	//核心入口
	SearchBar.render = function(options){
		var me = new Class(options);
		
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();
		//初始化检验项目
//      me.initTestItem();
        
		me.searchtool = form.render(me.searchConfig);
		//监听事件
		me.initListeners();
		return me;
	};
	//暴露接口
	exports(MOD_NAME,SearchBar);
});