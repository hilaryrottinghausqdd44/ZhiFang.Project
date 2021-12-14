

--检验结果表增加信息仪器结果ID等
Alter Table Lis_TestItem Add iResultState    int    null --结果编辑状态 默认0：无结果（或项目默认结果），1：仪器结果，2：人工编辑结果  
Alter Table Lis_TestItem Add iCommState        int    null   --通讯状态	iCommState	默认0：未进行通讯，1：双向发送，2：获取结果
