//部门下属员工
Ext.ns('Ext.manage');
Ext.define('Ext.manage.departmentstaff.bumenxiashuList', {
    extend:'Ext.zhifangux.GridPanel',
    alias:'widget.bumenxiashuList',
    title:'部门下属员工',
    width:825,
    height:200,
    objectName:'HREmployee',
    defaultLoad:false,
    defaultWhere:'',
    sortableColumns:true,
    initComponent:function() {
        var me = this;
        Ext.Loader.setConfig({enabled:true});
        Ext.Loader.setPath('Ext.zhifangux.GridPanel', getRootPath() + '/ui/zhifangux/GridPanel.js');
        me.url = getRootPath() + '/RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID?isPlanish=true&fields=HREmployee_CName,HREmployee_EName,HREmployee_IsEnabled,HREmployee_HRDept_Id,HREmployee_HRDept_CName,HREmployee_HRPosition_CName,HREmployee_Id,HREmployee_MobileTel,HREmployee_ExtTel,HREmployee_DispOrder,HREmployee_OfficeTel';
        me.searchArray = [ 'hremployee.CName', 'hremployee.HRDept.CName' ];
        me.store = me.createStore({
            fields:[ 'HREmployee_CName', 'HREmployee_EName', 'HREmployee_IsEnabled','HREmployee_HRDept_Id', 'HREmployee_HRDept_CName', 'HREmployee_HRPosition_CName', 'HREmployee_Id', 'HREmployee_MobileTel', 'HREmployee_ExtTel', 'HREmployee_DispOrder', 'HREmployee_OfficeTel' ],
            url:'RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID?isPlanish=true&fields=HREmployee_CName,HREmployee_EName,HREmployee_IsEnabled,HREmployee_HRDept_Id,HREmployee_HRDept_CName,HREmployee_HRPosition_CName,HREmployee_Id,HREmployee_MobileTel,HREmployee_ExtTel,HREmployee_DispOrder,HREmployee_OfficeTel',
            remoteSort:true,
            sorters:[ {
                property:'HREmployee_DispOrder',
                direction:'ASC'
            } ],
            PageSize:1e3,
            hasCountToolbar:true,
            buffered:false,
            leadingBufferZone:null
        });
        me.defaultColumns = [ {
            text:'名称',
            dataIndex:'HREmployee_CName',
            width:72,
            hideable:true,
            align:'left'
        }, {
            text:'英文名称',
            dataIndex:'HREmployee_EName',
            width:100,
            hideable:true,
            align:'left'
        }, {
            text:'在职',
            dataIndex:'HREmployee_IsEnabled',
            xtype:'booleancolumn',
            trueText:'是',
            falseText:'否',
            defaultRenderer:function(value) {
                if (value === undefined) {
                    return this.undefinedText;
                }
                if (!value || value === 'false' || value === '0' || value === 0) {
                    return this.falseText;
                }
                return this.trueText;
            },
            width:46,
            hideable:true,
            align:'left'
        },  {
            text:'直属部门id',
            dataIndex:'HREmployee_HRDept_Id',
            width:100,
            hideable:true,
            hidden:true,
            align:'left'
        },{
            text:'直属部门',
            dataIndex:'HREmployee_HRDept_CName',
            width:100,
            hideable:true,
            align:'left'
        }, {
            text:'职位',
            dataIndex:'HREmployee_HRPosition_CName',
            width:54,
            hideable:true,
            align:'left'
        }, {
            text:'主键ID',
            dataIndex:'HREmployee_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'手机号码',
            dataIndex:'HREmployee_MobileTel',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'分机',
            dataIndex:'HREmployee_ExtTel',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'显示次序',
            dataIndex:'HREmployee_DispOrder',
            width:66,
            hideable:true,
            align:'left'
        }, {
            text:'办公电话',
            dataIndex:'HREmployee_OfficeTel',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        } ];
        me.columns = me.createColumns();
        me.dockedItems = [ {
            itemId:'pagingtoolbar',
            xtype:'toolbar',
            dock:'bottom',
            items:[ {
                xtype:'label',
                itemId:'count',
                text:'共0条'
            } ]
        }, {
            xtype:'toolbar',
            itemId:'buttonstoolbar',
            dock:'top',
            items:[ {
                type:'refresh',
                itemId:'refresh',
                text:'更新',
                iconCls:'build-button-refresh',
                handler:function(but, e) {
                    var com = but.ownerCt.ownerCt;
                    com.load(true);
                }
            }, {
                type:'add',
                itemId:'add',
                text:'新增',
                iconCls:'build-button-add',
                handler:function(but, e) {
                    me.fireEvent('addClick');
                }
            }, '->', {
                xtype:'textfield',
                itemId:'searchText',
                width:160,
                emptyText:'名称/直属部门'
            }, {
                xtype:'button',
                text:'查询',
                itemId:'btnsearch',
                iconCls:'search-img-16 ',
                tooltip:'按照名称/直属部门进行查询',
                handler:function(button) {
                    me.fireEvent('searchClick');
                }
            } ]
        } ];
        me.deleteInfo = function(id, callback) {};
        me.addEvents('addClick');
        me.addEvents('searchClick');
        me.addEvents('afterOpenAddWin');
        this.callParent(arguments);
    },
    /**
     * 获取带查询参数的URL
     * @private
     * @return {}
     */
    getLoadUrl:function(){
    	var me = this;
		var w = '';
		if(me.externalWhere && me.externalWhere != ''){
			if(me.externalWhere.slice(-1) == '^'){
				w += me.externalWhere;
			}else{
				w += '' + me.externalWhere + ' and ';
			}
		}
		if(me.defaultWhere && me.defaultWhere != ''){
			w += '(' + me.defaultWhere.replace(/\%25/g,"%").replace(/\%27/g,"'") +') and ';
		}
		
		if(me.internalWhere && me.internalWhere != ''){
			w += '(' + me.internalWhere + ') and ';
		}
		w = w.slice(-5) == ' and ' ? w.slice(0,-5) : w;
		return me.url + '&where=' + w;
    }
});