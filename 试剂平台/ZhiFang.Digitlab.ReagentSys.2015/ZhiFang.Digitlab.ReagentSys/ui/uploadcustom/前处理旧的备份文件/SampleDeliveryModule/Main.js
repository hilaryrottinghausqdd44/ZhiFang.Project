/**
 * 初始化外送模块页面
 */
(function(){
    Ext.onReady(function(){
        Ext.QuickTips.init(); //初始化后就会激活提示功能
        Ext.Loader.setConfig({ enabled: true }); //允许动态加载
        Ext.create('Ext.container.Viewport',{
            layout: 'border',//出现过问题，去掉后 引用的js文件不显示
            border:false,
            items:[
                //{region: 'north',xtype:'Company',itemId:'actionbtntbar',height:205},
                {region: 'center',xtype:'tabpanel',itemId:'tabSamplemodule',height:65,items:[
                  {
			        title: '外送单位',
			        itemId: 'deliveryCompany',
                    layout:'border',
                    items:[                       
                           {region: 'north',xtype:'Company',layout:'fit',itemId:'gridCompany',height:200,width:'100%',border:true,
			                    split:true,
			                    //collapsible:true,
			                    //collapsed:false,
			                    defaultactive:false},                           
                           {region: 'center',xtype:'frmCompany',itemId:'frmId',header:false,width:'100%',height:180},
                           {region: 'south',xtype:'actionbtntbar',layout:'fit',itemId:'actionbtntbar',width:'100%',border:true,
			                    split:true,
			                    //collapsible:true,
			                    //collapsed:false,
			                    defaultactive:false}
                           
                        ]
			    }, {
			        title: '外送项目',
			        itemId: 'deliveryItems',
			        hidden: false,
                    layout:'border',
                    items:[
                         {region: 'west',xtype:'OutSideCompanyItem',itemId:'OutSideCompanyItem',header:false,border:true},  
                         {region: 'center',xtype:'OutSideItems',itemId:'OutSideItems',header:false,width:'70%'}
                    ]
			    }
                ]}
            ]
        })
    })
})()
