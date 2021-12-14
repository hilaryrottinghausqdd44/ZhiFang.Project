/**
 * @name：modules/pre/order/apply/list 医嘱单列表
 * @author：Jcall
 * @version 2020-06-18
 */
layui.extend({
	uxutil: 'ux/util',
	uxtable: 'ux/table',
	CommonSelectSickType: 'modules/common/select/sicktype'
}).define(['uxutil', 'laydate', 'uxtable', 'form', 'CommonSelectSickType'], function (exports) {
	"use strict";

	var $ = layui.$,
		laydate = layui.laydate,
		form = layui.form,
		uxtable = layui.uxtable,
		uxutil = layui.uxutil,
		CommonSelectSickType = layui.CommonSelectSickType,
		MOD_NAME = 'PreOrderApplyList';

	//获取医嘱单列表服务地址
	var GET_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/GetOrderList";
	//删除医嘱单服务地址
	var DEL_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/DeleteOrder";
	//审核医嘱单服务地址
	var CHECK_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/UpdateOrder";
	//取消审核医嘱单服务地址
	var UNCHECK_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/CancelOrder";

	//内部列表+表头dom
	var TABLE_DOM = [
		'<div class="layui-form" style="padding:2px;margin-bottom:2px;border:1px solid #e6e6e6;" lay-filter="formwhere">',
		'<div class="layui-row">',
		'<div class="layui-col-xs7">',
		'<label class="layui-form-label">开单日期</label>',
		'<div class="layui-input-block">',
		'<input type="text" name="{tableId}-date" id="{tableId}-date" class="layui-input">',
		'</div>',
		'</div>',
		'<div class="layui-col-xs4">',
		'<div class="layui-input-inlink" >',
		'<input type="checkbox" id="uncheck" name="uncheck" lay-filter="uncheck" title="未审核" lay-skin="primary" checked>',
		'</div>',
		'</div>',
		'</div>',
		'<div class="layui-row">',
		'<div class="layui-col-xs6">',
		'<label class="layui-form-label">姓名</label>',
		'<div class="layui-input-block">',
		'<input type="text" name="{tableId}-CName" id="{tableId}-CName" class="layui-input">',
		'</div>',
		'</div>',
		'<div class="layui-col-xs6">',
		'<label class="layui-form-label">病历号</label>',
		'<div class="layui-input-block">',
		'<input type="text" name="{tableId}-PatNo" id="{tableId}-PatNo" class="layui-input">',
		'</div>',
		'</div>',
		'</div>',
		'<div class="layui-row">',
		'<div class="layui-col-xs6">',
		'<label class="layui-form-label">就诊类型</label>',
		'<div class="layui-input-block">',
		'<select name="{tableId}-SickTypeID" id="{tableId}-SickTypeID" lay-filter="{tableId}-SickTypeID">',
		'<option value="">请选择</option>',
		'</select>',
		'</div>',
		'</div>',
		'</div>',
		'</div>',
		'<div class="{tableId}-table">',
		'<table class="layui-hide" id="{tableId}" lay-filter="{tableId}"></table>',
		'</div>',
		'<style>',
		'.layui-table-select{background-color:#5FB878;color:#fff;}',
		'.{tableId}-table .layui-table-body .layui-table-cell{height:80px !important;}',
		'.{tableId}-table .layui-table-body .layui-table-cell .layui-form-checkbox{margin-top:30px !important;}',
		'</style>'
	];

	//医嘱单列表
	var PreOrderApplyList = {
		tableId: null,//列表ID
		tableToolbarId: null,//列表功能栏ID
		//对外参数
		config: {
			domId: null,
			height: null,

			hisDeptNo: null,//HIS科室编码
			patno: null,//病历号
			sickTypeNo: null//就诊类型
		},
		//内部列表参数
		tableConfig: {
			elem: null,
			toolbar: null,
			skin: 'line',//行边框风格
			//even:true,//开启隔行背景
			size: 'sm',//小尺寸的表格
			defaultToolbar: null,
			height: 'full-4',
			defaultLoad: true,
			url: "",
			where: {},
			initSort: {
				field: 'DataAddTime',//排序字段
				type: 'desc'
			},
			cols: [[
				{ type: 'checkbox', fixed: 'left' },
				{ field: 'OrderFormID', title: '医嘱单ID', hide: true },
				{ field: 'OrderFormNo', title: '医嘱单号', hide: true },
				{ field: 'OrderTime', title: '开单日期', hide: true },
				{ field: 'IsAffirm', title: '是否已经审核', hide: true },
				{ field: 'CName', title: '病人姓名', hide: true },
				{ field: 'DeptName', title: '科室', hide: true },
				{ field: 'Bed', title: '病床', hide: true },
				{ field: 'PatNo', title: '病历号', hide: true },
				{ field: 'ItemName', title: '子项信息', hide: true },
				{
					field: 'Content', title: '内容', templet: function (d) {
						var status = [
							'<span class="layui-badge layui-bg-blue" style="margin-left:5px;">未审</span>',
							'<span class="layui-badge layui-bg-green" style="margin-left:5px;">已审</span>'
						];
						if (!d.DeptName) {
							d.DeptName = "";
						}
						if (!d.Bed) {
							d.Bed = "";
						}
						var html = [
							'<div>开单日期：' + d.OrderTime.replace('T', ' ') + status[d.IsAffirm] + '</div>',
							'<div>病人姓名：' + d.CName + ' <span style="margin-left:5px;">病历号：' + d.PatNo + '</span></div>',
							'<div>科室：' + d.DeptName + ' <span style="margin-left:5px;">病床：' + d.Bed + '</span></div>',
							'<div>子项信息：' + d.ItemName + '</div>'
						];
						return html.join('');
					}
				}
			]]
		}
	};
	//构造器
	var Class = function (setings) {
		var me = this;
		me.config = $.extend({}, me.config, PreOrderApplyList.config, setings);
		me.tableConfig = $.extend({}, me.tableConfig, PreOrderApplyList.tableConfig);

		if (me.config.height) {
			me.tableConfig.height = me.config.height;
		}
		if (me.config.hisDeptNo) { me.tableConfig.where.hisDeptNo = me.config.hisDeptNo; }
		if (me.config.patno) { me.tableConfig.where.patno = me.config.patno; }
		if (me.config.sickTypeNo) { me.tableConfig.where.sickTypeNo = me.config.sickTypeNo; }

		me.tableId = me.config.domId + "-table";
		me.tableConfig.elem = "#" + me.tableId;
		//数据渲染完的回调
		me.tableConfig.done = function (res, curr, count) {
			if (count > 0) {
				//默认选中第一行
				me.onClickFirstRow();
			}
		};
	};
	//初始化HTML
	Class.prototype.initHtml = function () {
		var me = this;
		var html = TABLE_DOM.join("").replace(/{tableId}/g, me.tableId).replace(/{tableToolbarId}/g, me.tableToolbarId);
		$('#' + me.config.domId).append(html);

		var newdate = uxutil.date.toString(uxutil.date.getDate(new Date()), true);
		var beforedate = uxutil.date.toString(uxutil.date.getNextDate(uxutil.date.getDate(new Date()), -7), true);
		//日期时间选择器
		laydate.render({
			elem: '#' + me.tableId + '-date'
			, range: true
			, value: beforedate + " - " + newdate,
			done: function (value, date, endDate) {
				me.onSearch();
			}
		});

		CommonSelectSickType.render({
			domId: me.tableId + '-SickTypeID',
			done: function () {
				if (me.config.sickTypeNo) {
					$("#" + me.tableId + "-SickTypeID").val(me.config.sickTypeNo);
					form.render();
				}
			}
		});
	};
	//监听事件
	Class.prototype.initListeners = function () {
		var me = this;

		//触发行单击事件
		me.uxtable.table.on('row(' + me.tableId + ')', function (obj) {
			if (me.checkedTr) {
				me.checkedTr.removeClass('layui-table-select');
			}
			me.checkedTr = obj.tr;
			me.checkedTr.addClass('layui-table-select');
		});
		//监听未审核复选框点击
		form.on('checkbox(uncheck)', function (data) {
			me.onSearch();
		});
		form.on('select(' + me.tableId + '-SickTypeID)', function (data) {
			me.onSearch();
		});
		//回车监听
		$(document).keydown(function (event) {
			switch (event.keyCode) {
				case 13://回车
					if (document.activeElement == document.getElementById(me.tableId + "-PatNo")) {
						var value = $("#" + me.tableId + "-PatNo").val();
						if (value) {
							me.onSearch();
						}
					}
					if (document.activeElement == document.getElementById(me.tableId + "-CName")) {
						var value = $("#" + me.tableId + "-CName").val();
						if (value) {
							me.onSearch();
						}
					}
					break;
			}
		});
	};
	//查询处理
	Class.prototype.onSearch = function () {
		var me = this;
		var data1 = form.val("formwhere");
		var where = "1=1";
		if (data1[me.tableId + "-PatNo"]) {
			me.tableConfig.where.patno = data1[me.tableId + "-PatNo"];
		}
		if (data1[me.tableId + "-SickTypeID"]) {
			me.tableConfig.where.sickTypeNo = data1[me.tableId + "-SickTypeID"];
		}
		if (data1["uncheck"] && data1["uncheck"] == "on") {
			where += " and (lisorderform.IsAffirm = 0 or lisorderform.IsAffirm is null)";
		}
		if (data1[me.tableId + '-date']) {
			var value = data1[me.tableId + '-date'];
			var nextdate = uxutil.date.toString(uxutil.date.getNextDate(value.split(" - ")[1], 1), true);
			where += " and (lisorderform.OrderTime >= '" + value.split(" - ")[0] + "' and lisorderform.OrderTime < '" + nextdate + "')";
		}
		if (data1[me.tableId + '-CName']) {
			var value = data1[me.tableId + '-CName'];
			where += " and lispatient.CName like '%" + value.split(" - ")[0]+ "%'";
		}
		me.tableConfig.where["strWhere"] = where;
		me.uxtable.reload({
			url: GET_LIST_URL,
			where:me.tableConfig.where
		});
	};
	//审核
	Class.prototype.onCheck = function(){
		var me = this,
			checkStatus = me.uxtable.table.checkStatus(me.tableId),
			list = checkStatus.data;
			
		if(list.length == 0){
			layer.alert("请选择医嘱进行操作！",{icon:5});
			return;
		}
		for(var i in list){
			if(list[i].IsAffirm == 1){//已审核
				layer.alert("存在已审核医嘱，请去掉勾选再进行操作！",{icon:5});
				return;
			}
		}
			
		var loadIndex = layer.load();//开启加载层	
		me._checkToServer(list,0,function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				layer.msg("审核成功！", { icon: 1 });
				me.onSearch();
			}else{
				layer.alert(data.msg || "缺失错误信息！",{icon:5});
			}
		});
	};
	//取消审核
	Class.prototype.onUnCheck = function(){
		var me = this,
			checkStatus = me.uxtable.table.checkStatus(me.tableId),
			list = checkStatus.data;
			
		if(list.length == 0){
			layer.alert("请选择医嘱进行操作！",{icon:5});
			return;
		}
		for(var i in list){
			if(list[i].IsAffirm == 0){//未审核
				layer.alert("存在未审核医嘱，请去掉勾选再进行操作！",{icon:5});
				return;
			}
		}
		
		var loadIndex = layer.load();//开启加载层
		me._unCheckToServer(list,0,function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				layer.msg("取消审核成功！", { icon: 1 });
				me.onSearch();
			}else{
				layer.alert(data.msg || "缺失错误信息！",{icon:5});
			}
		});
	};
	//删除处理
	Class.prototype.onDel = function(){
		var me = this,
			checkStatus = me.uxtable.table.checkStatus(me.tableId),
			list = checkStatus.data;
			
		if(list.length == 0){
			layer.alert("请选择医嘱进行操作！",{icon:5});
			return;
		}
		for(var i in list){
			if(list[i].IsAffirm == 1){//已审核
				layer.alert("已审核医嘱不能删除，请去掉勾选再进行操作！",{icon:5});
				return;
			}
		}
		
		var loadIndex = layer.load();//开启加载层
		me._delToServer(list,0,function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				layer.msg("删除医嘱单成功！", { icon: 1 });
				me.onSearch();
			}else{
				layer.alert(data.msg || "缺失错误信息！",{icon:5});
			}
		});
	};
	//审核提交到服务
	Class.prototype._checkToServer = function(list,index,callback,msgList){
		var me = this;
		var _msgList = [].concat(msgList || []);
		if(index >= list.length){//结束服务
			callback({
				success:_msgList.length == 0 ? true : false,
				msg:_msgList.join('<BR>')
			});
		}else{
			uxutil.server.ajax({
				url:CHECK_URL,
				data:{OrderFormNo:list[index].OrderFormNo}
			},function(data){
				if(!data.success){
					_msgList.push("【医嘱单号：" + list[index].OrderFormNo + "】" + data.msg);
				}
				me._checkToServer(list,++index,callback,_msgList);
			},true);
		}
	};
	//审核提交到服务
	Class.prototype._unCheckToServer = function(list,index,callback,msgList){
		var me = this;
		var _msgList = [].concat(msgList || []);
		if(index >= list.length){//结束服务
			callback({
				success:_msgList.length == 0 ? true : false,
				msg:_msgList.join('<BR>')
			});
		}else{
			uxutil.server.ajax({
				url:UNCHECK_URL,
				data:{OrderFormNo:list[index].OrderFormNo}
			},function(data){
				if(!data.success){
					_msgList.push("【医嘱单号：" + list[index].OrderFormNo + "】" + data.msg);
				}
				me._unCheckToServer(list,++index,callback,_msgList);
			},true);
		}
	};
	//审核提交到服务
	Class.prototype._delToServer = function(list,index,callback,msgList){
		var me = this;
		var _msgList = [].concat(msgList || []);
		if(index >= list.length){//结束服务
			callback({
				success:_msgList.length == 0 ? true : false,
				msg:_msgList.join('<BR>')
			});
		}else{
			uxutil.server.ajax({
				url:DEL_URL,
				data:{OrderFormNo:list[index].OrderFormNo}
			},function(data){
				if(!data.success){
					_msgList.push("【医嘱单号：" + list[index].OrderFormNo + "】" + data.msg);
				}
				me._delToServer(list,++index,callback,_msgList);
			},true);
		}
	};
	//默认选中第一行
	Class.prototype.onClickFirstRow = function(){
		var me = this;
		$("#" + me.tableId).find('.layui-table-main tr[data-index="0"]');
	};
	//核心入口
	PreOrderApplyList.render = function(options){
		var me = new Class(options);
		
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();		
		form.render();
		if (me.config.patno) {$("#" + me.tableId + "-PatNo").val(me.config.patno);}		
		//实例化列表
		me.uxtable = uxtable.render(me.tableConfig);
		//监听事件
		me.initListeners();
		me.onSearch();
		return me;
	};
	
	//暴露接口
	exports(MOD_NAME,PreOrderApplyList);
});