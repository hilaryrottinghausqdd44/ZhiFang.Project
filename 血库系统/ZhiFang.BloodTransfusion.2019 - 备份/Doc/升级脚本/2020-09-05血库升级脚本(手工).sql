

--�޸�ҽ����ҽ���ȼ�����������Ϊbigint
IF COL_LENGTH('Doctor', 'GradeNo') IS NOT NULL ALTER TABLE Doctor ALTER COLUMN GradeNo bigint;

----ɾ��ҽ���ȼ�������ϵ
ALTER TABLE [dbo].[Blood_docGrade] DROP CONSTRAINT [PK_Blood_docGrade];

--�޸�ҽ���ȼ���GradeNo��������Ϊbigint
IF COL_LENGTH('Blood_docGrade', 'GradeNo') IS NOT NULL ALTER TABLE Blood_docGrade ALTER COLUMN GradeNo [bigint] NOT NULL;

--���½���ҽ���ȼ�������ϵ
ALTER TABLE [dbo].[Blood_docGrade] ADD  CONSTRAINT [PK_Blood_docGrade] PRIMARY KEY CLUSTERED ([GradeNo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY];


