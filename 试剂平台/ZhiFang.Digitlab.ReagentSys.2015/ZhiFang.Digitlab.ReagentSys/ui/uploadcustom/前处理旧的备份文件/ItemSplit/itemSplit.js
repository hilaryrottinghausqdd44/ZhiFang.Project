/**
 * Created with JetBrains WebStorm.
 * User: 123
 * Date: 13-6-1
 * Time: 上午10:38
 * To change this template use File | Settings | File Templates.
 */
/**
 * 项目拆分界面
 */
Ext.define('itemSplit',{
    extend:'Ext.panel.Panel',
    alias:'widget.itemsplit',
    border:false,
    getCheckBoxItemServerUrl:'server/CheckedBoxList.txt',
    itemFields:["SectionName","SectionNo","ItemName","ComItemNO","EName"],
    ComItemField:["MEPTItemSplit_Par_CName","MEPTItemSplit_Par_Id","MEPTItemSplit_Par_SName"],
    GridItemFields:["MEPTItemSplit_NewBarCode","MEPTItemSplit_ItemAllItem_Id","MEPTItemSplit_ItemAllItem_CName","MEPTItemSplit_ItemAllItem_SName","MEPTItemSplit_MEPTSamplingGroup_CName"],
    /**
     * comItemPanel绑定数据的路径
     */
    ComItemListServerUrl:getRootPath()+"/MEPTService.svc/MEPT_UDTO_SearchMEPTItemSplitByHQL?isPlanish=true&fields=MEPTItemSplit_Par_CName,MEPTItemSplit_Par_Id,MEPTItemSplit_Par_SName",
    /**
     * comItemPanel列表头数据的URL
     */
    ComItemListHeaderServerUrl:'server/GetItemField.txt',
    TestItemListServerUrl:getRootPath()+"/MEPTService.svc/MEPT_UDTO_SearchMEPTItemSplitByHQL?isPlanish=true&fields=MEPTItemSplit_NewBarCode,MEPTItemSplit_ItemAllItem_Id,MEPTItemSplit_ItemAllItem_CName,MEPTItemSplit_ItemAllItem_SName,MEPTItemSplit_MEPTSamplingGroup_CName",
    objectRoot:'list',
    sectionNo:1,
    ItemData:null,
    layout:'fit',
    /**
     *初始化组件
     */
    initComponent:function(){
        var me=this;
        me.getTopStore();
        me.initView();
        me.callParent(arguments);
    },
    /**
     *初始化视图
     */
    initView:function(){
        var me = this;
        var top = me.createTopView();
        me.dockedItems=top;
        me.items=me.createBodyView();
    },
    createTopView:function(){
        var me=this;
        var com = {
            xtype: 'toolbar',
            border: false,
            itemId:'toolbarItem',
            items: [
                {
                    xtype: "checkboxfield",
                    margin:'5,0,1,1',
                    checked:true
                },{
                    xtype: "combobox",
                    fieldLabel: '当前小组',
                    width:190,
                    itemId:'currentItem',
                    labelWidth:60,
                    store: me.ItemData,
                    valueField:'ComItemNO',
                    displayField:'ItemName'
                },{
                    xtype:'button',
                    text: '添加',
                    margin:'5,40,1,1'//,
                    /*           handler:function(){
                     var value=this.ownerCt.getComponent('currentItem').getValue();
                     if(value==null)
                     {
                     Ext.Msg.alert('提示','请选择要添加的组合项目！');
                     }
                     else
                     {
                     var index= me.ItemData.find("ComItemNO",value);
                     var rec = new comItemData({
                     ItemName: me.ItemData.getAt(index).get('ItemName'),
                     EName: me.ItemData.getAt(index).get('EName'),
                     ComItemNO:me.ItemData.getAt(index).get('ComItemNO')
                     });
                     var gridComItem=Ext.getCmp('gridComItem');
                     gridComItem.store.insert(0, rec);
                     }
                     }*/
                },{
                    xtype: 'textfield',
                    fieldLabel:'定位要查看的项目',
                    labelWidth:110,
                    width:270,
                    listeners:{
                        specialkey: function (field, key) {
                            if (key.keyCode == 13) {
                                var value=this.getValue();
                                var store= Ext.getCmp("gridComItem").getStore();
                                store.filter('ItemName',value);
                            }
                        }
                    }
                }
            ]
        }
        return com;
    },
    createBodyView:function(){
        var me=this;
        var items=[{
            xtype:'panel',
            layout:'border',
            border:false,
            items: [{
                title: '拆分详细情况',
                region: 'center',     // position for region
                xtype: 'grid',
                forceFit:'true',
                id:'gridTestItem',
                split: true,         // enable resizing
                margins: '0 0 0 0',
                layout: 'fit',
                columns: [
                    {text:'条码号',dataIndex:'MEPTItemSplit_NewBarCode'},
                    {text:'采样组',dataIndex:'MEPTItemSplit_MEPTSamplingGroup_CName'},
                    {text:'项目编号',dataIndex:'MEPTItemSplit_ItemAllItem_Id'},
                    {text:'项目名称',dataIndex:'MEPTItemSplit_ItemAllItem_CName'},
                    {text:'项目简称',dataIndex:'MEPTItemSplit_ItemAllItem_SName'}
                ],
                store:me.getTestItemData()
            },{
                title: '需拆分条码的组合项目',
                forceFit:'true',
                region: 'west',     // center region is required, no width/height specified
                width: "35%",
                xtype: 'grid',
                id:'gridComItem',
                layout: 'fit',
                margins: '0 0 0 0',
                columns:[ {text:'项目编码',dataIndex:'MEPTItemSplit_Par_Id'},
                    {text:'项目名称',dataIndex:'MEPTItemSplit_Par_CName'},
                    {text:'项目简称',dataIndex:'MEPTItemSplit_Par_SName'},
                    {text:'操作', xtype:'actioncolumn', align:"center",
                        items: [{
                            iconCls:'build-button-delete hand',
                            tooltip:'删除',
                            handler:function(view,rowIndex,colIndex,item,e,record){
                                Ext.Msg.confirm("警告","确定要删除吗？",function (button){
                                    if(button == "yes"){
                                        /* me.deleteSampleTube(record);*/
                                    }
                                })
                            }
                        }]
                    }],
                store:me.GetComItemData(),
                listeners:{
                    selectionchange:function(th,selected){
                        var testItem= Ext.getCmp('gridTestItem');
                        var url=getRootPath()+"/MEPTService.svc/MEPT_UDTO_SearchMEPTItemSplitByHQL?isPlanish=true&fields=MEPTItemSplit_NewBarCode,MEPTItemSplit_ItemAllItem_Id," +
                            "MEPTItemSplit_ItemAllItem_CName,MEPTItemSplit_ItemAllItem_SName,MEPTItemSplit_MEPTSamplingGroup_CName&where=ParItemID="+selected[0].data.MEPTItemSplit_Par_Id;
                        testItem.store.proxy.url=url;
                        testItem.store.load();
                    }
                }
            }]
        }];
        return items;
    },
    /**
     *获取checkbox绑定的数据
     */
    getTopStore:function(){
        var me = this;
        var myItemStore=null;
        Ext.Ajax.request({
            async:false,//非异步
            url:me.getCheckBoxItemServerUrl,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    myItemStore=Ext.create('Ext.data.Store', {
                        fields:me.itemFields,
                        data:result,
                        proxy: {
                            type: 'memory',
                            params:'',
                            reader: {
                                type: 'json',
                                root:me.objectRoot
                            }
                        }
                    });
                    myItemStore.filter("SectionNo",me.sectionNo);
                }else{
                    Ext.Msg.alert('提示','获取信息失败！');
                }

            },
            failure : function(response,options){
                Ext.Msg.alert('提示','获取信息请求失败！'+response.responseText);
            }
        });
        me.ItemData=myItemStore;
    },
    /**
     *获取ComItemPanel绑定的数据
     */
    GetComItemData:function(){
        var me=this;
        var myStore = Ext.create('Ext.data.Store', {
            fields:me.ComItemField,
            proxy: {
                type: 'ajax',
                url: me.ComItemListServerUrl,
                reader: {
                    type: 'json',
                    root:  'ResultDataValue.List'
                },
                extractResponseData:function(response){
                    var data = Ext.JSON.decode(response.responseText);
                    var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                    data.ResultDataValue = ResultDataValue;
                    response.responseText = Ext.JSON.encode(data);
                    return response;
                }
            },
            autoLoad: true
        });
        me.comItemData=myStore;
        return myStore;
    },
    getTestItemData: function () {
        var me = this;
        var myStore = Ext.create('Ext.data.Store', {
            fields:me.GridItemFields,
            id:'itemDataStore',
            proxy: {
                type: 'ajax',
                url: me.TestItemListServerUrl,
                reader: {
                    type: 'json',
                    root:  'ResultDataValue.list'
                },
                extractResponseData:function(response){
                    var data = Ext.JSON.decode(response.responseText);
                    var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                    data.ResultDataValue = ResultDataValue;
                    response.responseText = Ext.JSON.encode(data);
                    return response;
                }
            },
            autoLoad: true
        });

        return myStore;
    }
})