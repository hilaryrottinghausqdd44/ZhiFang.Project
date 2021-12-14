/**
 * 加班清单
 * @author liangyl
 * @version 2017-1-24
 */
Ext.define('Shell.class.oa.at.attendance.statistical.empdetail.overtimelist.Grid', {
    extend: 'Shell.ux.grid.Panel',

    title: '加班清单列表',
    width: 800,
    height: 500,

    /**获取数据服务路径*/
    selectUrl: '/WeiXinAppService.svc/SC_UDTO_GetATEmpAttendanceEventLogDetailList',
    /**默认加载*/
    defaultLoad: true,
    defaultOrderBy: [{ property: 'ATEmpApplyAllLog_StartDateTime', direction: 'ASC' }],
    /**类型*/
    searchType:10601,
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        var buttonsToolbar = me.getComponent('buttonsToolbar');
        buttonsToolbar.on({
        	search:function(params){
        		me.onSearch();
        	}
        });
    },
    initComponent: function () {
        var me = this;
        //数据列
        me.columns = me.createGridColumns();
        me.callParent(arguments);
    },
        /**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		return Ext.create('Shell.class.oa.at.attendance.statistical.empdetail.travellist.SearchToolbar', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			searchType:me.searchType,
			items: items
		});
	},
    /**创建数据列*/
    createGridColumns: function () {
        var me = this;
        var columns = [{
            text: '申请时间', dataIndex: 'ATEmpApplyAllLog_DataAddTime', width: 135,
            sortable: false, defaultRenderer: true,isDate: true, hasTime: true
        }, {
            text: '姓名', dataIndex: 'ATEmpApplyAllLog_EmpName', width: 80,
            sortable: false, defaultRenderer: true
        }, {
            text: '工号', dataIndex: 'ATEmpApplyAllLog_Account', width: 80,
            sortable: false, defaultRenderer: true
        },  {
            text: '部门', dataIndex: 'ATEmpApplyAllLog_HRDeptCName', width: 100,
            sortable: false, defaultRenderer: true
        },  {
            text: '职务', dataIndex: 'ATEmpApplyAllLog_HRPositionCName', width: 70,
            sortable: false, defaultRenderer: true
        }, {
            text: '开始时间', dataIndex: 'ATEmpApplyAllLog_StartDateTime', width: 85,
            sortable: false, defaultRenderer: true
        },{
            text: '结束时间', dataIndex: 'ATEmpApplyAllLog_EndDateTime', width: 85,
            sortable: false, defaultRenderer: true
        },  {
            text: '加班时长', dataIndex: 'ATEmpApplyAllLog_EvenLength', width: 60,
            sortable: false, defaultRenderer: true
        }, {
            text: '加班事由', dataIndex: 'ATEmpApplyAllLog_Memo', width: 150,
            sortable: false, defaultRenderer: true
        },{
            text: '审批状态', dataIndex: 'ATEmpApplyAllLog_ApproveStatusName', width: 70,
            sortable: false, defaultRenderer: true
        },{
            text: '审批人', dataIndex: 'ATEmpApplyAllLog_ApproveName', width: 70,
            sortable: false, defaultRenderer: true
        },{
            text: '审批时间', dataIndex: 'ATEmpApplyAllLog_ApproveDateTime', width: 135,
            sortable: false, defaultRenderer: true,isDate: true, hasTime: true
        },{
            text: '审批意见', dataIndex: 'ATEmpApplyAllLog_ApproveMemo', width: 150,
            sortable: false, defaultRenderer: true
        },{
            text: '审批详情', dataIndex: 'ATEmpApplyAllLog_Memo', width: 150,hidden:true,
            sortable: false, defaultRenderer: true
        },{
            text: '主键ID', dataIndex: 'ATEmpApplyAllLog_Id', isKey: true, hidden: true, hideable: false
        }];

        return columns;
    },
    /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [],par=[];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&');
		var	params = me.getComponent('buttonsToolbar').getParams();
		if(params){
			par.push("searchType=" + me.searchType);
			par.push("attypeId=" + params.ATEmpApplyAllLog );
			par.push("deptId=" + params.DeptID);
			par.push("isGetSubDept=" + params.isCheckTree);
			par.push("empId=" +  params.UserID);
			par.push("startDate=" + params.applySDate);
			par.push("endDate=" + params.applyEDate);
		    par.push("approveStatusID=" + params.ApproveStatusID);
			url += par.join("&");
        }
		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";

		if (where) {
			url += '&where=' + JShell.String.encode(where);
		}
		return url;
	}
});