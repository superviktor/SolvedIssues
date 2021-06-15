1 Run rabbitmq id docker
 docker run -d --hostname my-rabbit --name myrabbit -e RABBITMQ_DEFAULT_USER=guest -e RABBITMQ_DEFAULT_PASS=1234 -p 5672:5672 -p 15672:15672 rabbitmq:3-management