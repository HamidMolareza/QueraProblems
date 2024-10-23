from django.db import models

from django.contrib.auth.models import User


class Category(models.Model):
    name = models.CharField(max_length=100)

    parent = models.ForeignKey('self', null=True, blank=True, on_delete=models.CASCADE, related_name='subcategories')

    def __str__(self):
        return self.name


class RoleType(models.IntegerChoices):
    MEMBER = 0, 'member'

    OWNER = 1, 'owner'


class Project(models.Model):
    title = models.CharField(max_length=255)

    description = models.TextField()

    category = models.ForeignKey(Category, related_name='projects', on_delete=models.SET_NULL, null=True)

    members = models.ManyToManyField(User, through='Membership', related_name='projects')

    created_at = models.DateTimeField(auto_now_add=True)

    updated_at = models.DateTimeField(auto_now=True)

    class Meta:
        ordering = ['-updated_at']

    def __str__(self):
        return self.title


class Membership(models.Model):
    person = models.ForeignKey(User, on_delete=models.CASCADE, related_name='memberships')

    project = models.ForeignKey(Project, on_delete=models.CASCADE, related_name='memberships')

    role = models.IntegerField(choices=RoleType.choices, default=RoleType.MEMBER)

    class Meta:
        constraints = [
            models.UniqueConstraint(fields=['person', 'project'], name='unique_membership')
        ]
