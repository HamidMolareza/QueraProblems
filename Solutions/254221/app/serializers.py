from rest_framework import serializers

from django.contrib.auth.models import User

from .models import Project, Category


class ProjectSerializer(serializers.ModelSerializer):
    members = serializers.PrimaryKeyRelatedField(many=True, queryset=User.objects.all())

    class Meta:
        model = Project

        fields = ['title', 'description', 'category', 'members', 'created_at', 'updated_at']
