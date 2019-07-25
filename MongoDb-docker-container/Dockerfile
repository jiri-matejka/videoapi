FROM mongo
# the Mongo image uses root
COPY --chown=root entrypoint.sh /usr/local/bin
RUN chmod +x /usr/local/bin/entrypoint.sh
ENTRYPOINT [ "/bin/bash", "/usr/local/bin/entrypoint.sh"]