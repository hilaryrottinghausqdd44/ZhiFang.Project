/**
 * 微信帐号密码重置--病人微信帐号
 * @author longfc
 * @version 2017-12-19
 */
Ext.define('Shell.class.weixin.resetpwd.PatientGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],

	title: '病人帐号 ',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchBWeiXinAccountByHQL?isPlanish=true',
	/**获取数据服务(过滤医生微信帐号)路径*/
	selectUrl2: '/ServerWCF/WeiXinAppService.svc/WXADS_SearchWeiXinAccount_User?isPlanish=true',

	/**下载医生相片*/
	DownLoadImageUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DownLoadBWeiXinAccountImageByAccountID',
	/**默认加载*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: false,
	/**是否启用修改按钮*/
	hasEdit: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	//hasSave:true,
	/**是否启用查询框*/
	hasSearch: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			width: 185,
			emptyText: '用户名称',
			isLike: true,
			itemId: 'search',
			fields: ['bweixinaccount.UserName']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '所属省份',
			dataIndex: 'BWeiXinAccount_ProvinceName',
			width: 65,
			align: 'center',
			sortable: false,
			menuDisabled: true
		}, {
			text: '所属城市',
			dataIndex: 'BWeiXinAccount_CityName',
			width: 65,
			align: 'center',
			sortable: false,
			menuDisabled: true
		}, {
			text: '用户名称',
			dataIndex: 'BWeiXinAccount_UserName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '手机号码',
			dataIndex: 'BWeiXinAccount_MobileCode',
			hidden: true,
			width: 90,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: 'Email',
			dataIndex: 'BWeiXinAccount_Email',
			hidden: true,
			width: 90,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'BWeiXinAccount_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			xtype: 'actioncolumn',
			text: '密码重置',
			align: 'center',
			width: 65,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					meta.tdAttr = 'data-qtip="<b style=\'color:#dd6572\'>随机生成密码</b>"';
					meta.style = 'background-color:#dd6572';
					return 'button-lock hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.openReset(rec);
					//me.onPwdReset(rec);
				}
			}]
		}, {
			xtype: 'actioncolumn',
			text: '个人照片',
			align: 'center',
			width: 60,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-show hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					if(id) {
						me.DownLoadImage(id, 'PersonImage');
					}

				}
			}]
		}];
		return columns;
	},
	/**下载*/
	DownLoadImage: function(Id, imageType) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.DownLoadImageUrl;
		url += '?accountID=' + Id + '&operateType=1&imageType=' + imageType;
		window.open(url);
	},
	openReset:function(rec){
		var me=this;
		var id = rec.get("BWeiXinAccount_Id");
		JShell.Win.open('Shell.class.weixin.resetpwd.EditPwd', {
			resizable: false,
			maximizable:false,//是否带最大化功能
			formtype:'edit',
			PK:id,
			listeners:{
				pwdReset:function(p){
					p.close();
				}
			}
		}).show();
	}
});