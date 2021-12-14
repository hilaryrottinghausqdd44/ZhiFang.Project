/**
 * 员工月考勤的部门员工列表
 * @author longfc
 * @version 2016-07-27
 */
Ext.define('Shell.class.oa.at.attendance.monthlog.UserGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	title: '部门员工',
	width: 1200,
	height: 600,
	/**登录员工*/
	EMPID: JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1,
	/**根据部门ID查询模式*/
	DeptTypeModel: false,

	/**获取数据服务路径*/
	selectUrl: '/RBACService.svc/RBAC_UDTO_GetHREmployeeByManagerEmpId?isPlanish=true',

	/**修改服务地址*/
	editUrl: '/RBACService.svc/RBAC_UDTO_UpdateHREmployeeByField',
	/**删除数据服务路径*/
	delUrl: '/RBACService.svc/RBAC_UDTO_DelHREmployee',

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: false,

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 1000,

	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 200,

	/**复选框*/
	multiSelect: false,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: false,
	/**是否启用修改按钮*/
	hasEdit: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否启用查询框*/
	hasSearch: false,

	/**是否存在上级*/
	hasManager: false,
    /*数据范围部门：depth，公司：all*/
	datarangetype: "",
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'HREmployee_HRDept_CName',
		direction: 'ASC'
	}, {
		property: 'HREmployee_CName',
		direction: 'ASC'
	}],
	initComponent: function() {
		var me = this;
		me.addEvents('addclick');
		//查询框信息
		me.searchInfo = {
			width: '98%',
			emptyText: '员工/部门名称',
			isLike: true,
			itemId: 'search',
			fields: ['hremployee.CName', 'hremployee.HRDept.CName']
		};
		me.buttonToolbarItems = [{
			type: 'search',
			info: me.searchInfo
		}];
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '隶属部门',
			dataIndex: 'HREmployee_HRDept_CName',
			width: 110,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '员工姓名',
			dataIndex: 'HREmployee_CName',
			//width: 120,
			flex: 1,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'HREmployee_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '时间戳',
			dataIndex: 'HREmployee_DataTimeStamp',
			hidden: true,
			sortable: false,
			menuDisabled: true,
			hideable: false
		}];

		if(me.hasManager) {
			columns.push({
				text: '直接上级',
				dataIndex: 'HREmployee_ManagerName',
				width: 80,
				hidden: true,
				sortable: false,
				menuDisabled: true,
				defaultRenderer: true
			});
		}
		return columns;
	},
	onSaveClick: function() {},
	onAddClick: function() {},

	loadByDeptId: function(id) {
		var me = this;
		me.DeptId = id;
		me.onSearch();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = buttonsToolbar.getComponent('search'),
			arr = [],
			url = '';

		if(search) {
			var value = search.getValue();
			me.internalWhere = value ? me.getSearchWhere(value) : '';
		}

		url += JShell.System.Path.getUrl(me.selectUrl);

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');

		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if(where) where = "(" + where + ")";

		if(where) {
			url += ' &where= ' + JShell.String.encode(where);
		}
		if (me.datarangetype) {
		    url += ' &type= ' + me.datarangetype;
		}
		return url;
	},
	changeShowType: function(value) {
		var me = this;
		me.DeptTypeModel = value ? false : true;
		me.onSearch();
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {

		return data;
	},
	mergeCells: function(grid, rows) {
		var gridEl = Ext.get(grid.getId() + "-body").first();
		var arrayTr = gridEl.select("tr");
		arrayTr.each(function(el) {
			if(el.getAttribute("class") == "x-grid-header-row") {
				arrayTr.removeElement(el)
			}
		})
		var merge = function(rowspanObj, removeObjs) {
			var trIndex = 1;
			arrayTr.each(function(tr) {
				var arrayTd = tr.select("td");
				arrayTd.each(function(td) {
					if(td.getAttribute("class").indexOf("x-grid-cell-special") != -1) {
						arrayTd.removeElement(td)
					}
				});
				if(trIndex == rowspanObj["tr"]) {
					var tdIndex = 1;
					arrayTd.each(function(td) {
						if(tdIndex == rowspanObj["td"]) {
							if(rowspanObj["rowspan"] != 1) {
								td.set({
									"rowspan": rowspanObj["rowspan"],
									"valign": "middle"
								});
							}
						}
						tdIndex++;
					});
				}
				Ext.each(removeObjs, function(obj) {
					var tdIndex = 1;
					if(trIndex == obj["tr"]) {
						arrayTd.each(function(td) {
							if(tdIndex == obj["td"]) {
								td.set({
									"style": "display:none"
								})
							}
							tdIndex++;
						})
					}
				})
				trIndex++;
			})
		}
		var rowspanObj = {};
		var removeObjs = [];
		Ext.each(rows, function(rowIndex) {
			var trIndex = 1;
			var rowspan = 1;
			var divHtml = null; //单元格内的数值
			var trCount = arrayTr.getCount();
			arrayTr.each(function(tr) {
				//准备td集合
				var arrayTd = tr.select("td");
				arrayTd.each(function(td) {
						//移除序号,多选框等不进行合并的td
						if(td.getAttribute("class").indexOf("x-grid-cell-special") != -1) {
							arrayTd.removeElement(td)
						}
					})
					//准备格式化每一列
				var tdIndex = 1;
				arrayTd.each(function(td) {
					if(tdIndex == rowIndex) {
						if(!divHtml) {
							divHtml = td.first().getHTML();
							rowspanObj = {
								tr: trIndex,
								td: tdIndex,
								rowspan: rowspan
							}
						} else {
							var cellText = td.first().getHTML();
							if(cellText == divHtml) {
								rowspanObj["rowspan"] = rowspanObj["rowspan"] + 1;
								removeObjs.push({
									tr: trIndex,
									td: tdIndex
								});
								if(trIndex == trCount) {
									merge(rowspanObj, removeObjs); //执行合并函数
								}
							} else {
								merge(rowspanObj, removeObjs); //执行合并函数
								divHtml = cellText;
								rowspanObj = {
									tr: trIndex,
									td: tdIndex,
									rowspan: rowspan
								}
								removeObjs = [];
							}
						}
					}
					tdIndex++;
				})
				trIndex++;
			})
		})
	}
});