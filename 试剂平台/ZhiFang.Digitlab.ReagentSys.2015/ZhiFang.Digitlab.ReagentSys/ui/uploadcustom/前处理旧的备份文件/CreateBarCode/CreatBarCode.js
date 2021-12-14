/**
 * Created with JetBrains WebStorm.
 * User: 123
 * Date: 13-5-27
 * Time: 下午1:53
 * To change this template use File | Settings | File Templates.
 */
Ext.define('CreateBarCode',{
    extend:'Ext.tab.Panel',
    alias:'widget.createbarcode',
    bodyCls:'bg-white',   //控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
    /***
     * 条码方式选择
     * @return {}
     */
     createBarcodeChoose:function(){
        var item={
            xtype:'radiogroup',
            columns:3,
            vertical: true,
            items: [
                { boxLabel: '现打条码', name: 'rb', inputValue: '1', checked: true },
                { boxLabel: '预制条码', name: 'rb', inputValue: '2'},
                { boxLabel: '预制条码和现打条码 以采样管设置为准',width:125, name: 'rb', inputValue: '3' }
            ]
           };
        return item;            
     },
    /***
     * 现打条码规则
     * @return {}
     */
     createTabpanelBarcodeRule:function(){
        var me=this;
        var item={
            title:'现打条码规则',
            items:[{
                xtype:'form',
                layout:'absolute',
                bodyCls:'bg-white',
                frame:true,
                height:600,
                width:600,
                border:false,
                items :[{
                    x:20,
                    xtype:'checkboxgroup',
                    border:false,
                    columns:1,
                    items: [
                        { boxLabel  : '固定条码'},
                        { boxLabel  : '采样管编码'},
                        { boxLabel  : '采样组编码'},
                        { boxLabel  : '日期编码'}
                    ]
                },{
                    x:150,
                    xtype: 'textfield'

                },{
                    x:150,
                    y:70,
                    xtype:'radiogroup',
                    columns:1,
                    vertical: true,
                    items: [
                        { boxLabel: '6位YYMMDD', name: 'rb',inputValue: '1',checked: true },
                        { boxLabel: '5位YMMDD 年取最后一位', name: 'rb',inputValue: '2'},
                        { boxLabel: '5位YYMDD 月（1_9,A,B,C）',name: 'rb', inputValue: '3' },
                        { boxLabel: '4位YMDD 月（1_9,A,B,C）',name: 'rb',inputValue: '4'},
                        { boxLabel: '3位YMD 日（1_9,A,V）',name: 'rb',inputValue: '5'}
                    ]

                },{
                    x:20,
                    y:185,
                    xtype:'checkboxgroup',
                    border:false,
                    columns:1,
                    items: [
                        { boxLabel  : '自增条码编号的位数',checked: true },
                        { boxLabel  : '条码序号是否累计 不累计则每天从1开始'},
                        { boxLabel  : '采用检验单号 从第'},
                        { boxLabel  : '一张检验单拆分为多个条码管编码位数'},
                        { boxLabel  : '外送条码加前缀'}
                    ]
                },{
                    x:170,
                    y:185,
                    xtype: 'textfield'

                },{
                    x:156,
                    y:232,
                    xtype: 'textfield',
                    width:70

                },{
                    x:230,
                    y:235,
                    xtype: 'label',
                    text:'位起取'

                },{
                    x:270,
                    y:232,
                    xtype: 'textfield',
                    width:60

                },{
                    x:290,
                    y:258,
                    xtype: 'textfield',
                    width:60

                },{
                    x:200,
                    y:284,
                    xtype: 'textfield'

                }]
            }]
         };
        return item;            
     }, 
    /***
     * 条码生成方式
     * @return {}
     */
    createBarcode:function(){
        var me=this;
        var item={
            title: '条码生成方式',
            items:[{
                xtype:'form',
                height:600,
                bodyCls:'bg-white',
                width:600,
                frame:true,
                items:[{
                    xtype:'fieldset',
                    title: '条码方式选择',
                    items :[me.createBarcodeChoose()]
                },{
                    xtype:'tabpanel',
                    border:false,
                    items:[me.createTabpanelBarcodeRule(),
                     {
                     title:'检验单号自动生成规则'

                    }]
                }]
            }]
        };
        return item;
    },
    initComponent:function(){
        var me =this;
        me.initView();
        me.callParent(arguments);
    },
    initView:function(){
        var me =this;
        me.items=[
        me.createBarcode(),{
            title: '条码打印格式设置'
        },{
            title: '采样组打印格式设置'
        }];
    }

});