/**
 * 微信帐号密码重置--医生微信帐号
 * @author longfc
 * @version 2017-12-19
 */
Ext.define('Shell.class.weixin.resetpwd.DoctorGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],

	title: '医生信息列表 ',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchBDoctorAccountByHQL?isPlanish=true',

	/**下载医生相片*/
	DownLoadImageUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DownLoadBDoctorAccountImageByAccountID',
	/**默认加载*/
	defaultLoad: true,
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
	/**区域*/
	AreaID: null,
	/**查询栏参数设置*/
	searchToolbarConfig: {},
	/**医院*/
	HospitalID: '',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			width: 185,
			emptyText: '医生名称/医生工号',
			isLike: true,
			itemId: 'search',
			fields: ['bdoctoraccount.Name', 'bdoctoraccount.HWorkNumberID']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '所属医院',
			dataIndex: 'BDoctorAccount_HospitalName',
			width: 120,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '医生名称',
			dataIndex: 'BDoctorAccount_Name',
			width: 90,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '医生工号',
			dataIndex: 'BDoctorAccount_HWorkNumberID',
			width: 90,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '科室名称',
			dataIndex: 'BDoctorAccount_HospitalDeptName',
			width: 90,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '次序',
			dataIndex: 'BDoctorAccount_DispOrder',
			hidden: true,
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true
		}, {
			text: '主键ID',
			dataIndex: 'BDoctorAccount_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '微信主键ID',
			dataIndex: 'BDoctorAccount_WeiXinUserID',
			width: 170,
			hidden: true //,hideable:false
		}, {
			text: '专业级别',
			dataIndex: 'BDoctorAccount_ProfessionalAbilityName',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '银行种类',
			dataIndex: 'BDoctorAccount_BankID',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '银行帐号',
			dataIndex: 'BDoctorAccount_BankAccount',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '密码重置',
			align: 'center',
			width: 65,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var WeiXinUserID = record.get('BDoctorAccount_WeiXinUserID');
					if(WeiXinUserID) {
						meta.tdAttr = 'data-qtip="<b style=\'color:#dd6572\'>随机生成密码</b>"';
						meta.style = 'background-color:#dd6572';
						return 'button-lock hand';
					} else {
						meta.tdAttr = 'data-qtip="<b style=\'color:#dd6572\'>该医生还没有微信帐号</b>"';
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.openReset(rec);
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
		}, {
			xtype: 'actioncolumn',
			text: '职业证书',
			align: 'center',
			width: 60,
			hidden: true,
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
						me.DownLoadImage(id, 'ProfessionalAbility');
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
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,
			params = [];

		me.internalWhere = '';
		if(buttonsToolbar) {
			search = buttonsToolbar.getComponent('search').getValue();
		}
		//根据医院查询
		if(me.HospitalID) {
			params.push("bdoctoraccount.HospitalID in (" + me.HospitalID + ")");
		}
		//如果区域不为空，医院列表为空
		if(me.AreaID && !me.HospitalID) {
			params.push("bdoctoraccount.HospitalID=null");
		}
		params.push("bdoctoraccount.WeiXinUserID is not null");

		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	},
	openReset: function(rec) {
		var me = this;
		var id = rec.get("BDoctorAccount_WeiXinUserID");
		JShell.Win.open('Shell.class.weixin.resetpwd.EditPwd', {
			resizable: false,
			maximizable: false, //是否带最大化功能
			formtype:'edit',
			PK: id,
			listeners: {
				pwdReset: function(p) {
					p.close();
				}
			}
		}).show();
	}
});