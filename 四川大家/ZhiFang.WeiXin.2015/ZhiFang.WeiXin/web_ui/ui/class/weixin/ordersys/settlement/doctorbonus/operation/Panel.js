/**
 * 医生奖金结算操作记录
 * @author longfc
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.operation.Panel', {
	extend: 'Ext.panel.Panel',
	title: '操作记录',
	autoScroll: true,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorBonusOperationByHQL?isPlanish=false',

	bodyPadding: 10,
	classNameSpace: 'ZhiFang.WeiXin.Entity',
	className: 'OSDoctorBonusFormStatus',
	/**业务对象ID*/
	PK: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(JShell.System.ClassDict.OSDoctorBonusFormStatus) {
			me.onLoadData();
		} else {
			JShell.System.ClassDict.init(me.classNameSpace, me.className, function() {
				me.onLoadData();
			});
		}
	},
	/**获取操作记录信息*/
	clearData: function(info) {
		var me = this;
		me.PK = "";
		var html = '<div style="color:red;text-align:center;margin:20px 10px;font-weight:bold;">' + info + '</div>';
		me.update(html);
	},
	/**获取操作记录信息*/
	onLoadData: function(callback) {
		var me = this;
		if(!me.PK) {
			me.clearData("操作记录信息为空");
		} else {
			var url = JShell.System.Path.getRootUrl(me.selectUrl);

			var fields = ['Id', 'BobjectID', 'Type', 'Memo', 'DataAddTime', 'CreatorName'];
			url += '&fields=' + fields.join(',');
			url += '&where=BobjectID=' + me.PK;
			url += '&sort=[{"property":"DataAddTime","direction":"ASC"}]';

			JcallShell.Server.get(url, function(data) {
				if(data.success) {
					if(data.value && data.value.list.length > 0) {
						me.changeHtml(data.value.list);
					} else {
						me.clearData("没有操作记录");
					}
				} else {
					me.clearData(data.msg);
				}
			});
		}
	},
	/**更改页面内容*/
	changeHtml: function(list) {
		var me = this,
			arr = list || [],
			len = arr.length,
			html = [];
		if(len && len > 0) {
			for(var i = 0; i < len; i++) {
				var data = arr[i];
				html.push('<div style="margin:5px;">');
				html.push(JShell.Date.toString(data.DataAddTime) + ' ');
				html.push(data.CreatorName + ' ');

				var info = JShell.System.ClassDict.getClassInfoById(me.className, data.Type);
				if(info) {
					var style = [];
					if(info.BGColor) {
						style.push('color:' + info.BGColor);
					}
					html.push('<b style="' + style.join(';') + '">' + info.Name + '</b> ');

					if(data.PTaskOperLog_OperateMemo) {
						html.push('处理意见：<b>' + data.Memo + '</b>');
					}
					html.push('</div>');
				}
				html.push('</div>');
			}
		}
		me.update(html.join(''));
	}
});