FROM mysql:latest

ENV MYSQL_ROOT_PASSWORD=app123
ENV MYSQL_DATABASE=catalog1

COPY init.sql /docker-entrypoint-initdb.d/

EXPOSE 3306

CMD ["mysqld"]
