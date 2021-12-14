//alert("MainController.js");
Ext.define("AM.controller.MainController",{
	extend:'Ext.app.Controller',
	views:['MenuTreeView','TabPanel'],//视图
	stores:['MenuTreeStore'],//数据集合
	models:['MenuTree'],//模型
	init:function(){
		//控制代码
		this.control({
			/*----------左树点击动作----------*/
			'menuTreeView':{
				itemmousedown: this.loadMenu
			}
		});
	},
	loadMenu:function(selModel, record){ 
        if (record.get('leaf')) {  
            var panel = Ext.getCmp(record.get('text'));
            if(!panel){  
                panel ={  
                    title: record.get('text'),  
                    icon:record.get('icon'),
                    html: "<html><body><iframe src='"+record.get('url')+"' style='height:100%;width:100%' frameborder='no'></iframe></body></html>",
                    closable: true  
                };
                this.openTab(panel,record.get('text')); 
            }else{ 
                var main = Ext.getCmp("content-panel"); 
                main.setActiveTab(panel);  
            } 
        }  
    }, 
    openTab : function (panel,id){  
        var o = (typeof panel == "string" ? panel : id || panel.id); 
        var main = Ext.getCmp("content-panel"); 
        var tab = main.getComponent(o);
        if (tab) {
            main.setActiveTab(tab);  
        } else if(typeof panel!="string"){  
            panel.id = o;  
            var p = main.add(panel);  
            main.setActiveTab(p);  
        }  
    }
})