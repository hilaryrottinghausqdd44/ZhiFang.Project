Ext.define("Shell.class.siteQuery.addEmpDept.addPanel", {
    extend: 'Shell.ux.panel.AppPanel',
    width: 1100,
    height: 600,
    title:'科室配置',
    layout: 'border',
    AddUrl: '/ServiceWCF/DictionaryService.svc/AddEmpDeptLinks',
    deptGridRecord:'',
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.dockedItems = me.createDockedItems();
        me.callParent(arguments);
    },

    createDockedItems: function () {
            var me = this;
            var tooblar = Ext.create('Ext.toolbar.Toolbar', {
                width: 100,
                items: [{
                    xtype: 'button', text: '保存',
                    iconCls: 'button-save',
                    listeners: {
                        click: function () {
                            var rs = me.InsertColumnsTempale();
                            me.fireEvent("save",me,rs);
                            window.location.href = Shell.util.Path.uiPath + '/class/siteQuery/';
                        }
                    }
                }]
            });
            return [tooblar];
    },
    createItems: function () {
        var me = this;
        //判断用户是否登录
		function getCookie(name) {
				var cookies = document.cookie;
				var list = cookies.split("; ");    // 解析出名/值对列表
				      
				for(var i = 0; i < list.length; i++) {
					var arr = list[i].split("=");  // 解析出名和值
					if(arr[0] == name)
						return decodeURIComponent(arr[1]);  // 对cookie值解码
				} 
				return "";
			}
		var UserNo = getCookie("UserNo");
		var UserCName = getCookie("UserCName");
		var ShortCode = getCookie("ShortCode");
	  	var cookie = {UserNo:UserNo,CName:UserCName,ShortCode:ShortCode}
		me.deptGridRecord=cookie;
        me.columnsList = Ext.create("Shell.class.siteQuery.addEmpDept.select", {
            region: 'west',
            itemId: 'columnsList',
            title:'待选列',
            width: '40%',
            listeners: {
                itemclick: function (m, record, item, index) {
                    var addColumns = me.getComponent("addColumns");
                    var store = addColumns.getStore();
                    var newData = record.data;
                    store.loadData([newData], true);
                    me.getComponent("columnsList").store.removeAt(index);
                }
            }
        });
        me.columnsAdd = Ext.create("Shell.class.siteQuery.addEmpDept.unselect", {
            region: 'center',
            itemId:'addColumns',
            title: '已选列',
            listeners: {
                rebockColumns: function (m, r) {
                    var addColumns = me.getComponent("columnsList");
                    var store = addColumns.getStore();
                    store.loadData([r], true);
                }
            },
            width: 500
        });
        return [me.columnsList,me.columnsAdd];
    },
    InsertColumnsTempale: function () {
        var me = this;
        var arr = [];
        var addColumns = me.getComponent("addColumns");
        var store = addColumns.getStore();
        store.each(function (record) {
            arr.push({
                "EDLID": 0,
                "UserNo": me.deptGridRecord.UserNo,
                "DeptNo": record.get("DeptNo"),
                "UserCName": me.deptGridRecord.CName,
                "ShortCode": me.deptGridRecord.ShortCode,
                "DeptCName": record.get("CName")
            });
        });
        var rs = "";
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + me.AddUrl,
            async: false,
            method: 'POST',
            params: Ext.encode({entity : arr }),
            success: function (response, options) {
                 rs = Ext.JSON.decode(response.responseText);
            }
        });
        return rs;
    }
});