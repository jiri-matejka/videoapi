#!/bin/bash

echo "Removing whole MongoDb database"
cd /data/db
rm -r *

# mongo image itself also uses trick with entrypoint.sh, we need to call it
# instead of calling mongod directly.
# The script will create user according to environment variables.
# https://github.com/docker-library/mongo/blob/master/4.0/Dockerfile

/bin/bash "docker-entrypoint.sh" mongod