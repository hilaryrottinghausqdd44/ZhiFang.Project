/**
 * Created with JetBrains WebStorm.
 * User: 123
 * Date: 13-5-21
 * Time: 上午11:09
 * To change this template use File | Settings | File Templates.
 */
/**
 * 采样组对应的项目维护界面
 */
Ext.define('SampleItemSelect', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.sampleitemselect',
    layout: 'border',
    frame: false,
    border:false,
    objectFields: ["MEPTSamplingGroup_Id","MEPTSamplingGroup_CName","MEPTSamplingGroup_SName","MEPTSamplingGroup_Shortcode","MEPTSamplingGroup_DispOrder","MEPTSamplingGroup_Destination","MEPTSamplingGroup_MEPTSamplingGroupPrint_PrinterChannelCode1","MEPTSamplingGroup_MEPTSamplingGroupPrint_PrintNum","MEPTSamplingGroup_MEPTSamplingGroupPrint_IsPasteTube","MEPTSamplingGroup_BSpecialty_Name","MEPTSamplingGroup_MEPTSamplingTube_CName","MEPTSamplingGroup_MEPTSamplingTube_Capacity","MEPTSamplingGroup_MEPTSamplingTube_MinCapacity","MEPTSamplingGroup_MEPTSamplingTube_MEPTBCharge_CName","MEPTSamplingGroup_MEPTSamplingTube_MEPTBCharge_Comment1","MEPTSamplingGroup_MEPTSamplingTube_MEPTBCharge_Comment2","MEPTSamplingGroup_MEPTSamplingTube_MEPTBCharge_Comment3","MEPTSamplingGroup_BSampleType_Name"],
    objectFieldsItems: ["MEPTSamplingItem_MEPTSamplingGroup_Id","MEPTSamplingItem_ItemAllItem_Id","MEPTSamplingItem_ItemAllItem_CName","MEPTSamplingItem_ItemAllItem_EName"],
    //获取样本小组的后台服务地址
    getSampleServerUrl:getRootPath()+"/MEPTService.svc/MEPT_UDTO_SearchMEPTSamplingGroupByHQL?fields=MEPTSamplingGroup_Id,MEPTSamplingGroup_CName,MEPTSamplingGroup_SName,MEPTSamplingGroup_Shortcode,MEPTSamplingGroup_DispOrder,MEPTSamplingGroup_Destination,MEPTSamplingGroup_MEPTSamplingGroupPrint_PrinterChannelCode1,MEPTSamplingGroup_MEPTSamplingGroupPrint_PrintNum,MEPTSamplingGroup_MEPTSamplingGroupPrint_IsPasteTube,MEPTSamplingGroup_BSpecialty_Name,MEPTSamplingGroup_MEPTSamplingTube_CName,MEPTSamplingGroup_MEPTSamplingTube_Capacity,MEPTSamplingGroup_MEPTSamplingTube_MinCapacity,MEPTSamplingGroup_MEPTSamplingTube_MEPTBCharge_CName,MEPTSamplingGroup_MEPTSamplingTube_MEPTBCharge_Comment1,MEPTSamplingGroup_MEPTSamplingTube_MEPTBCharge_Comment2,MEPTSamplingGroup_MEPTSamplingTube_MEPTBCharge_Comment3,MEPTSamplingGroup_BSampleType_Name&where=1=1&isPlanish=true",
    //获取样本项目的后台服务地址
    getSampleItemServerUrl: getRootPath()+"/MEPTService.svc/MEPT_UDTO_SearchMEPTSamplingItemByHQL?fields=MEPTSamplingItem_MEPTSamplingGroup_Id,MEPTSamplingItem_ItemAllItem_Id,MEPTSamplingItem_ItemAllItem_CName,MEPTSamplingItem_ItemAllItem_EName&where=1=1&isPlanish=true",
    selectFiled:'MEPTSamplingGroup_Id',
    initComponent: function () {
        var me = this;
        //初始化视图
        me.initView();
        me.callParent(arguments);
    },
    /**
     * 初始化视图
     */
    initView: function () {
        var me = this;
        //效果展示区
        var center = me.createCenter();
        //属性面板
        var east = me.createEast();
        center.region = "center";
        east.region="east";
        east.width=600;
        me.items = [center,east];
        var top = me.createTop();
        me.dockedItems = [top];
    },
    /**
     * 列表头部的查询条件
     */
    createTop: function () {
        var me=this;
        var com = {
            xtype: 'toolbar',
            border: false,
            items: [{
                xtype: 'textfield',
                fieldLabel:'采样组编码',
                itemId:"txtSearch",
                labelWidth:70,
                listeners:{
                    specialkey: function (field, key) {
                        //回车键快捷方式的实现
                        if (key.keyCode == 13) {
                            var btn = this.getValue();
                            var store=me.getComponent('gridSample').getStore();
                            //筛选数据
                            var id=btn;
                            if(btn!="")
                            {
                                id=btn
                            }else{
                                id=store.data.items[0].data.MEPTSamplingGroup_Id;
                            }
                            store.filter(me.selectFiled,btn);
                            var store1 = me.getComponent('gridItem');//.getStore();
                            var url=getRootPath()+"/MEPTService.svc/MEPT_UDTO_SearchMEPTSamplingItemByHQL?fields=MEPTSamplingItem_MEPTSamplingGroup_Id,MEPTSamplingItem_ItemAllItem_Id,MEPTSamplingItem_ItemAllItem_CName,MEPTSamplingItem_ItemAllItem_EName&where=SamplingGroupID="+id+"&isPlanish=true";
                            store1.store.proxy.url=url;
                            store1.store.load();
                            store.clearFilter(true);

                        }
                    },
                    render: function (field, key) {
                        //文本框获取焦点
                        this.focus(true, 200);
                    }
                }
            }]
        }
        return com;
    },
    /**
     *采样组列表的视图
     */
    createCenter:function(){
            var me=this;
            var com = {
                xtype: 'grid',
                preventHeader:true,
                itemId:"gridSample",
		        forceFit:true,
                columns: [
                    {text:'采样组编号',dataIndex:'MEPTSamplingGroup_Id'},
                    {text:'采样组名称',dataIndex:'MEPTSamplingGroup_CName'},
                    {text:'采样组简称',dataIndex:'MEPTSamplingGroup_SName'},
                    {text:'输入代码',dataIndex:'MEPTSamplingGroup_Shortcode'}
                ],
                store: me.getSampleData()
            };
            com.listeners={
                selectionchange:function(){
                    var rows = this.getSelectionModel().getSelection(); //getSelection();获取当前选中的记录数组
                    var t1 = me.getComponent('gridItem');//.getStore();
                    var id=rows[0].data.MEPTSamplingGroup_Id;
                    var url=getRootPath()+"/MEPTService.svc/MEPT_UDTO_SearchMEPTSamplingItemByHQL?fields=MEPTSamplingItem_MEPTSamplingGroup_Id,MEPTSamplingItem_ItemAllItem_Id,MEPTSamplingItem_ItemAllItem_CName,MEPTSamplingItem_ItemAllItem_EName&where=SamplingGroupID="+id+"&isPlanish=true";
                    t1.store.proxy.url=url;
                    t1.store.load();
                }
            };
            com.store.load({
            callback:function(r){// 回调函数
                var store1=me.getComponent('gridItem');//.getStore();
                var id=r[0].data.MEPTSamplingGroup_Id;
                var url=getRootPath()+"/MEPTService.svc/MEPT_UDTO_SearchMEPTSamplingItemByHQL?fields=MEPTSamplingItem_MEPTSamplingGroup_Id,MEPTSamplingItem_ItemAllItem_Id,MEPTSamplingItem_ItemAllItem_CName,MEPTSamplingItem_ItemAllItem_EName&where=SamplingGroupID="+id+"&isPlanish=true";
                store1.store.proxy.url = url;
                store1.store.load();
            }
        });
            return com;
        },
    /**
     * 获取采样小组数据
     */
    getSampleData: function () {
        var me = this;
        var myStore = Ext.create('Ext.data.Store', {
            fields: me.objectFields,
            proxy: {
                type: 'ajax',
                url: me.getSampleServerUrl,
                reader: {
                    type: 'json',
                    root:  'ResultDataValue.list'
                },
                extractResponseData:function(response){
                    var data = Ext.JSON.decode(response.responseText);
                    if(data.ResultDataValue!=""){
                        var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                        data.ResultDataValue = ResultDataValue;
                        response.responseText = Ext.JSON.encode(data);
                    }
                    return response;
                }
            },
            autoLoad: true
        });
        return myStore;
    },
    /**
     *采样项目列表的视图
     */
    createEast:function(){
        var me = this;
        var com2 = {
            xtype: 'grid',
            title: '列表',
            preventHeader:true,
            itemId:'gridItem',
	        forceFit:true,
            columns: [
             /*   {text:'采样组编号',dataIndex:'MEPTSamplingItem_MEPTSamplingGroup_Id'},*/
                {text:'项目编号',dataIndex:'MEPTSamplingItem_ItemAllItem_Id'},
                {text:'项目名称',dataIndex:'MEPTSamplingItem_ItemAllItem_CName'},
                {text:'英文名称',dataIndex:'MEPTSamplingItem_ItemAllItem_EName'}
            ],
            store:me.getSampleItemData()
        };
        return com2;
    },
    /**
     *获取采样小组项目数据
     */
    getSampleItemData: function () {
        var me = this;
        var myStore = Ext.create('Ext.data.Store', {
            fields: me.objectFieldsItems,
            proxy: {
                type: 'ajax',
                url: me.getSampleItemServerUrl,
                reader: {
                    type: 'json',
                    root:  'ResultDataValue.list'
                },
                extractResponseData:function(response){
                    var data = Ext.JSON.decode(response.responseText);
                    if(data.ResultDataValue!=""){
                        var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                        data.ResultDataValue = ResultDataValue;
                        response.responseText = Ext.JSON.encode(data);
                    }
                    return response;
                }
            },
            autoLoad: true
        });
        return myStore;
    }


});