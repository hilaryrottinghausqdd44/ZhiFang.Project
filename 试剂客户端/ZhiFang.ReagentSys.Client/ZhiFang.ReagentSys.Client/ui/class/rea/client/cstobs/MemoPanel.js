/**
 * CS试剂客户端升级BS
 * @author longfc
 * @version 2018-10-17
 */
Ext.define('Shell.class.rea.client.cstobs.MemoPanel', {
	extend: 'Ext.panel.Panel',
	bodyPadding: 0,
	title: 'CS试剂客户端升级BS说明',
	width: 420,
	User: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.createhtmlStr(null);
		me.callParent(arguments);
	},
	createDefaultHtml: function(cenorg) {
		var me = this;
		var cssStr = '<link rel="stylesheet" type="text/css" href="../web_src/bootstrap-3.3.2-dist/css/bootstrap.min.css" />';
		me.DefaultHtml = cssStr + '<h1></h1>' +
			'<p style="margin:10px; font-size:12px;">首先:将机构授权初始化完成;</p>' +
			'<p style="margin:10px; font-size:12px;" >第一步:在CS试剂客户端升级BS向导里进行【部门信息导入】操作;</p>' +
			'<p style="margin:10px; font-size:12px;">第二步:在CS试剂客户端升级BS向导里进行【部门人员导入】操作;</p>' +
			'<p style="margin:10px; font-size:12px;">第三步:在CS试剂客户端升级BS向导里进行【人员帐号导入】操作;</p>' +
			'<p style="margin:10px; font-size:12px;">第四步:在CS试剂客户端升级BS向导里进行【角色信息导入】操作;</p>' +
			'<p style="margin:10px; font-size:12px;">第五步:在CS试剂客户端升级BS向导里进行【供应商信息导入】操作;</p>' +
			'<p style="margin:10px; font-size:12px;">第六步:在CS试剂客户端升级BS向导里进行【库房货架信息导入】操作;</p>' +
			'<p style="margin:10px; font-size:12px;">第七步:在CS试剂客户端升级BS向导里进行【仪器信息导入】操作;</p>' +
			'<p style="margin:10px; font-size:12px;">第八步:在CS试剂客户端升级BS向导里进行【机构货品信息导入】操作;</p>' +
			'<p style="margin:10px; font-size:12px;">第九步:在CS试剂客户端升级BS向导里进行【仪器试剂信息导入】操作;</p>' +
			'<p style="margin:10px; font-size:12px;">第十步:在CS试剂客户端升级BS向导里进行【供货商货品信息导入】操作;</p>' +
			'<p style="margin:10px; font-size:12px;">第十一步:在CS试剂客户端升级BS向导里进行【库存数据导入】操作;</p>' +
			'<p style="margin:10px; font-size:12px;">最后:完成CS试剂客户端升级BS向导操作;</p>';
	},
	createhtmlStr: function(cenorg) {
		var me = this;
		var cssStr = '<link rel="stylesheet" type="text/css" href="../web_src/bootstrap-3.3.2-dist/css/bootstrap.min.css" />';
		if(!me.DefaultHtml) {
			me.DefaultHtml = cssStr + '<div class="container-fluid" style="margin:10px;">' +
				'<div class="row">' +
				'<div class="col-md-6">' +
				'<b>第一步操作:</b><br/>首先:将机构授权初始化完成;<br/>【将CS试剂客户端部门信息导入BS】处理<br/>' +
				'</div>' +
				'<div class="col-md-6">' +
				'<b>第二步操作:</b><br/>【将CS试剂客户端部门人员导入BS】处理<br/>【将CS试剂客户端人员帐号导入BS】处理' +
				'</div>' +
				'</div>' +
				'<br/>' +

				'<div class="row">' +
				'<div class="col-md-6">' +
				'<b>第三步操作:</b><br/>【将CS试剂客户端角色信息导入BS】处理<br/>【将CS试剂客户端供应商信息导入BS】处理<br/>' +
				'</div>' +
				'<div class="col-md-6">' +
				'<b>第四步操作:</b><br/>【将CS试剂客户端库房信息导入BS】处理<br/>【将CS试剂客户端货架信息导入BS】处理' +
				'</div>' +
				'</div>' +
				'<br/>' +

				'<div class="row">' +
				'<div class="col-md-6">' +
				'<b>第五步操作:</b><br/>【将CS试剂客户端仪器信息导入BS】处理<br/>【将CS试剂客户端机构货品信息导入BS】处理<br/>' +
				'</div>' +
				'<div class="col-md-6">' +
				'<b>第六步操作:</b><br/>【将CS试剂客户端仪器试剂信息导入BS】处理<br/>【将CS试剂客户端供应商货品导入BS】处理' +
				'</div>' +
				'</div>' +
				'<br/>' +

				'<div class="row">' +
				'<div class="col-md-12">' +
				'<b>最后操作:</b><br/>【库存数据导入,并将库存试剂信息转换为BS临时入库单】处理<br/>'+
				'1.先删除当前机构的原来的入库及库存信息;<br/>'+
				'2.将库存试剂信息转换为BS的临时入库单;<br/>'+
				'3.在库存初始化对临时入库单进行调整及入库确认,产生库存试剂信息。' +
				'</div>' +
				'</div>' +

				'</div>';
		}
		me.update(me.DefaultHtml);
	},
	loadData: function(cenorg) {
		var me = this;
		me.createhtmlStr(cenorg);
	}
});