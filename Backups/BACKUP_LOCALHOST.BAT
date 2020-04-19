@echo off
mysqldump --user=root --single-transaction --password=root --host=localhost --port=3306 --opt icredit > COPIA.SQL
